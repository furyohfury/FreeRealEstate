using System;
using System.Collections.Generic;
using Game.Auth;
using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class SessionSystem : NetworkBehaviour
    {
        public event Action<PlayerData> OnPlayerJoined;
        public NetworkList<PlayerData> PlayerDatas => _playerDatas;

        private PlayerFactory _playerFactory;
        private LobbySystem _lobbySystem;
        private AuthorizationSystem _authorizationSystem;

        private readonly NetworkList<PlayerData> _playerDatas = new NetworkList<PlayerData>();
        private readonly List<PlayerData> _pendingPlayerDatas = new List<PlayerData>();

        [Inject]
        public void Construct(PlayerFactory playerFactory, LobbySystem lobbySystem, AuthorizationSystem authorizationSystem)
        {
            _authorizationSystem = authorizationSystem;
            _lobbySystem = lobbySystem;
            _playerFactory = playerFactory;
        }

        private void Awake()
        {
            // Обязательно подписываемся на изменения, если клиентам нужно реагировать локально
            _playerDatas.OnListChanged += OnPlayerListChanged;
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;

            RegisterLobbyPlayerRpc(_authorizationSystem.IsAuthorized
                ? _authorizationSystem.PlayerId
                : $"Player_{NetworkManager.Singleton.LocalClientId}");

            Debug.Log("[sessionsystem] RegisterLobbyPlayerRpc");
        }

        [Rpc(SendTo.Server)]
        private void RegisterLobbyPlayerRpc(string playerId, RpcParams rpcParams = default)
        {
            var players = _lobbySystem.SessionInfo.Players;
            var clientId = rpcParams.Receive.SenderClientId;

            for (int i = 0, count = players.Count; i < count; i++)
            {
                LobbyPlayerInfo lobbyPlayerInfo = players[i];
                string nickname = lobbyPlayerInfo.Nickname;

                if (lobbyPlayerInfo.Id == playerId)
                {
                    _pendingPlayerDatas.Add(new PlayerData
                                            {
                                                clientID = clientId,
                                                Nickname = nickname
                                            });
                }
            }

            if (_playerDatas.Count <= 0 && _pendingPlayerDatas.Count == _lobbySystem.SessionInfo.Players.Count)
            {
                foreach (var playerData in _pendingPlayerDatas)
                {
                    SpawnPlayerObject(playerData.clientID, playerData.Nickname.ToString());
                }
            }
        }

        private void SpawnPlayerObject(ulong clientId, string nickname)
        {
            Player player = _playerFactory.SpawnPlayer(clientId);

            var playerData = new PlayerData
                             {
                                 clientID = clientId,
                                 NetworkObjID = player.NetworkObjectId,
                                 Nickname = nickname // Пример работы с FixedString
                             };

            // Теперь это автоматически синхронизируется с клиентами!
            _playerDatas.Add(playerData);
        }

        private void OnPlayerListChanged(NetworkListEvent<PlayerData> changeEvent)
        {
            // Триггерится и на сервере, и на клиентах при добавлении/удалении элементов
            if (changeEvent.Type == NetworkListEvent<PlayerData>.EventType.Add)
            {
                OnPlayerJoined?.Invoke(changeEvent.Value);
            }
        }

        public void LaunchNextRound()
        {
            if (NetworkManager.Singleton.IsServer == false)
                return;

            foreach (var playerData in _playerDatas)
            {
                NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(playerData.clientID);
                if (networkObject == null)
                    continue;

                Player player = networkObject.GetComponent<Player>();
                Debug.Log($"Setting player {playerData.clientID} hp to max");
                player.Health.Value = player.MaxHealth.Value;
                Debug.Log($"player {playerData.clientID} hp = {player.Health.Value}, max = {player.MaxHealth.Value}");
                player.GetToSpawnPosition();
            }
        }

        public PlayerData GetPlayerData(ulong clientId)
        {
            for (int i = 0, count = _playerDatas.Count; i < count; i++)
            {
                if (_playerDatas[i].clientID == clientId)
                {
                    return _playerDatas[i];
                }
            }

            return default(PlayerData);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _playerDatas.OnListChanged -= OnPlayerListChanged;
        }
    }
}
