using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class ZenjectNetworkObjectSpawner : IInitializable, IDisposable
    {
        private readonly Dictionary<NetworkObject, ZenjectPrefabInstanceHandler> _map = new Dictionary<NetworkObject, ZenjectPrefabInstanceHandler>();
        private readonly DiContainer _container;
        private readonly NetworkPrefabsList[] _networkPrefabsLists;

        public ZenjectNetworkObjectSpawner(DiContainer container, NetworkPrefabsList[] networkPrefabsLists)
        {
            _networkPrefabsLists = networkPrefabsLists;
            _container = container;
        }

        public void Initialize()
        {
            for (int i = 0, count = _networkPrefabsLists.Length; i < count; i++)
            {
                NetworkPrefabsList networkPrefabsList = _networkPrefabsLists[i];

                for (int j = 0, count1 = networkPrefabsList.PrefabList.Count; j < count1; j++)
                {
                    NetworkPrefab networkPrefab = networkPrefabsList.PrefabList[j];
                    var networkObject = networkPrefab.Prefab.GetComponent<NetworkObject>();
                    var handler = new ZenjectPrefabInstanceHandler(_container, networkObject);
                    _map.Add(networkObject, handler);
                    NetworkManager.Singleton.PrefabHandler.AddHandler(networkObject, handler);
                }
            }
        }

        public NetworkObject SpawnPrefab(
            ulong clientId,
            NetworkObject prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null)
        {
            NetworkObject spawnedObj = CoreSpawn(clientId,
                prefab,
                pos,
                rot,
                parent);
            if (spawnedObj != null)
            {
                spawnedObj.Spawn();
            }
            return spawnedObj;
        }

        public NetworkObject SpawnPrefab(
            NetworkObject prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null)
        {
            return SpawnPrefab(NetworkManager.Singleton.LocalClientId,
                prefab,
                pos,
                rot,
                parent);
        }

        public T SpawnPrefab<T>(
            ulong clientId,
            T prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null) where T : NetworkBehaviour
        {
            NetworkObject spawnedObj = CoreSpawn(clientId,
                prefab,
                pos,
                rot,
                parent);
            if (spawnedObj == null)
                return null;

            spawnedObj.Spawn();
            return spawnedObj.GetComponent<T>();
        }

        public T SpawnPrefab<T>(
            T prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null) where T : NetworkBehaviour
        {
            return SpawnPrefab(NetworkManager.Singleton.LocalClientId,
                prefab,
                pos,
                rot,
                parent);
        }

        public T SpawnPlayerPrefab<T>(
            ulong clientId,
            T prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null) where T : NetworkBehaviour
        {
            NetworkObject spawnedObj = CoreSpawn(clientId,
                prefab,
                pos,
                rot,
                parent);
            if (spawnedObj == null)
                return null;

            spawnedObj.SpawnAsPlayerObject(clientId);
            return spawnedObj.GetComponent<T>();
        }

        public T SpawnPlayerPrefab<T>(
            T prefab,
            Vector3 pos,
            Quaternion rot,
            Transform parent = null) where T : NetworkBehaviour
        {
            return SpawnPlayerPrefab(NetworkManager.Singleton.LocalClientId,
                prefab,
                pos,
                rot,
                parent);
        }

        /// <summary>
        /// Общая логика для поиска NetworkObject, работы с Zenject хэндлерами и фабрикации объекта.
        /// </summary>
        private NetworkObject CoreSpawn(
            ulong clientId,
            Component prefab, // Используем Component, чтобы принимать и NetworkObject, и T (NetworkBehaviour)
            Vector3 pos,
            Quaternion rot,
            Transform parent)
        {
            if (!prefab.TryGetComponent(out NetworkObject networkObject))
            {
                Debug.LogError("Cant get NO from prefab");
                return null;
            }

            // Используем TryGetValue для избежания повторного поиска в Dictionary при добавлении
            if (!_map.TryGetValue(networkObject, out var handler))
            {
                handler = CreateNewHandlerClientRpc(networkObject);
            }

            return InstantiateNetworkObject(clientId,
                pos,
                rot,
                handler,
                parent);
        }

        [ClientRpc]
        private ZenjectPrefabInstanceHandler CreateNewHandlerClientRpc(NetworkObject networkObject)
        {
            var handler = new ZenjectPrefabInstanceHandler(_container, networkObject);
            _map.Add(networkObject, handler);
            NetworkManager.Singleton.PrefabHandler.AddHandler(networkObject, handler);

            return handler;
        }

        private static NetworkObject InstantiateNetworkObject(
            ulong clientId,
            Vector3 pos,
            Quaternion rot,
            ZenjectPrefabInstanceHandler handler,
            Transform parent)
        {
            NetworkObject spawnedObj = handler.Instantiate(clientId, pos, rot);
            spawnedObj.transform.parent = parent;
            return spawnedObj;
        }

        public void Dispose()
        {
            if (NetworkManager.Singleton == null)
                return;

            foreach (ZenjectPrefabInstanceHandler handler in _map.Values)
            {
                NetworkManager.Singleton.PrefabHandler.RemoveHandler(handler.GetNetworkObject());
            }
        }
    }
}
