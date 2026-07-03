using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class PlayersProviderController : IInitializable, IDisposable
    {
        private readonly PlayersProvider _playersProvider;
        private readonly SessionSystem _sessionSystem;
        private readonly NetworkManager _networkManager;

        public PlayersProviderController(SessionSystem sessionSystem, PlayersProvider playersProvider, NetworkManager networkManager)
        {
            _sessionSystem = sessionSystem;
            _playersProvider = playersProvider;
            _networkManager = networkManager;
        }

        public void Initialize()
        {
            // _sessionSystem.OnPlayerJoined += OnPlayerJoined;
            // _networkManager.OnClientStarted += OnClientStarted;
        }

        private void OnClientStarted()
        {
            IReadOnlyList<NetworkObject> playerObjects = _networkManager.SpawnManager.PlayerObjects;

            for (int i = 0, count = playerObjects.Count; i < count; i++)
            {
                AddPlayer(playerObjects[i]);
            }
        }

        private void OnPlayerJoined(PlayerData data)
        {
            Dictionary<ulong, NetworkObject> spawnedObjects = _networkManager.SpawnManager.SpawnedObjects;
            ulong objID = data.NetworkObjID;

            if (spawnedObjects.TryGetValue(objID, out NetworkObject networkObject))
            {
                AddPlayer(networkObject);
            }
            else
            {
                Debug.LogError($"Player {objID} not found");
            }
        }

        private void AddPlayer(NetworkObject networkObject)
        {
            var objID = networkObject.NetworkObjectId;

            if (networkObject.TryGetComponent(out Player player))
            {
                if (networkObject.IsOwner)
                {
                    _playersProvider.MyPlayer = player;
                    Debug.Log($"My Player {objID} added to provider");
                }
                else
                {
                    _playersProvider.AddOtherPlayer(player);
                    Debug.Log($"Other Player {objID} added to provider");
                }
            }
            else
            {
                Debug.LogError($"Player {objID} not found");
            }
        }

        public void Dispose()
        {
            _sessionSystem.OnPlayerJoined -= OnPlayerJoined;
            _networkManager.OnClientStarted -= OnClientStarted;
        }
    }
}
