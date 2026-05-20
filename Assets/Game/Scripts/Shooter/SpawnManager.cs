using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Game
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField]
        private Transform[] _spawnPoints;
        [SerializeField]
        private Transform _container;
        private int _nextSpawnIndex = 0;

        private void Start()
        {
            if (NetworkManager.Singleton != null)
            {
                // Подписываемся на событие подключения клиента
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            // Спавнить имеет право ТОЛЬКО сервер
            if (!NetworkManager.Singleton.IsServer) return;

            // Выбираем точку спавна
            Transform spawnPoint = _spawnPoints[_nextSpawnIndex];
            _nextSpawnIndex = (_nextSpawnIndex + 1) % _spawnPoints.Length;

            // Инстанцируем префаб в нужных координатах
            GameObject playerInstance = Instantiate(_playerPrefab, spawnPoint.position, spawnPoint.rotation);

            // Передаем объект в сеть и назначаем ему владельца (clientId)
            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            
            if (playerInstance.TryGetComponent<NetworkTransform>(out var networkTransform))
            {
                // В новых версиях NGO это заставит компонент принудительно взять текущие transform-координаты
                playerInstance.transform.position = spawnPoint.position;
                playerInstance.transform.rotation = spawnPoint.rotation;
                networkTransform.AuthorityMode = NetworkTransform.AuthorityModes.Owner;
            }
        }
    }
}
