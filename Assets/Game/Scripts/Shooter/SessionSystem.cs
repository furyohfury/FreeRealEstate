using System;
using System.Collections.Generic;
using Game.Scripts.Shooter;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class SessionSystem : NetworkBehaviour, IInitializable, IDisposable
    {
        public event Action<PlayerData> OnPlayerJoined;
        
        // Для чтения снаружи предоставляем NetworkList
        public NetworkList<PlayerData> PlayerDatas => _playerDatas;

        private PlayerFactory _playerFactory;
        
        // Переходим на NetworkList
        private readonly NetworkList<PlayerData> _playerDatas = new NetworkList<PlayerData>();

        [Inject]
        public void Construct(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        // NetworkList требует инициализации в Awake или OnNetworkSpawn
        public void Initialize()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            
            if (NetworkManager.Singleton.IsServer)
            {
                foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
                {
                    OnClientConnected(clientId);
                }
            }
        }

        private void Awake()
        {
            // Обязательно подписываемся на изменения, если клиентам нужно реагировать локально
            _playerDatas.OnListChanged += OnPlayerListChanged;
        }

        private void OnPlayerListChanged(NetworkListEvent<PlayerData> changeEvent)
        {
            // Триггерится и на сервере, и на клиентах при добавлении/удалении элементов
            if (changeEvent.Type == NetworkListEvent<PlayerData>.EventType.Add)
            {
                OnPlayerJoined?.Invoke(changeEvent.Value);
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if (NetworkManager.Singleton.IsServer == false) return;

            Player player = _playerFactory.SpawnPlayer(clientId);
            
            var playerData = new PlayerData
                             {
                                 clientID = clientId,
                                 NetworkObjID = player.NetworkObjectId,
                                 Nickname = $"Player_{clientId}" // Пример работы с FixedString
                             };
                             
            // Теперь это автоматически синхронизируется с клиентами!
            _playerDatas.Add(playerData); 
        }

        public void LaunchNextRound()
        {
            if (NetworkManager.Singleton.IsServer == false) return;

            foreach (var playerData in _playerDatas)
            {
                NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(playerData.clientID);
                if (networkObject == null) continue;
                
                Player player = networkObject.GetComponent<Player>();
                Debug.Log($"Setting player {playerData.clientID} hp to max");
                player.Health.Value = player.MaxHealth.Value;
                Debug.Log($"player {playerData.clientID} hp = {player.Health.Value}, max = {player.MaxHealth.Value}");
                player.GetToSpawnPosition();
            }
        }

        public void Dispose()
        {
            _playerDatas.OnListChanged -= OnPlayerListChanged;
            if (NetworkManager.Singleton != null)
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }
}