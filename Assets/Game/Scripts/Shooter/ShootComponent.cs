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
            Debug.Log($"[SERVER] RPC получен! Текущий таймер: {_timer}");

            if (_timer <= 0)
            {
                Debug.Log("[SERVER] Кулдаун пройден. Пытаюсь заспавнить пулю...");
                _timer = _cooldown;
                NetworkObject projectile = Instantiate(_projectilePrefab, _firePoint.position, transform.rotation);
                projectile.Spawn();
            }
        }
    }
}
