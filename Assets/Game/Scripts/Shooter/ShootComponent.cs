using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class ShootComponent : NetworkBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private NetworkObject _projectilePrefab;
        [SerializeField]
        private float _cooldown;
        private float _timer;

        public override void OnNetworkSpawn()
        {
            _timer = _cooldown;
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        }

        [ServerRpc]
        public void ShootServerRpc()
        {
            if (_timer <= 0)
            {
                _timer = _cooldown;
                NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_projectilePrefab, position: _firePoint.position,
                    rotation: transform.rotation);
                // GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.Euler(transform.forward));
            }
        }
    }
}
