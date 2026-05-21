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
        private AimPointComponent _aimPointComponent;
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

                Vector3 targetPoint = _aimPointComponent.GetAimPoint();

                // Вычисляем направление от дула оружия к нашей точке прицеливания
                Vector3 aimDirection = (targetPoint - _firePoint.position).normalized;

                NetworkObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.LookRotation(aimDirection));
                projectile.Spawn();
            }
        }
    }
}
