using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class ShootComponent : NetworkBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private Projectile _projectilePrefab;
        [SerializeField]
        private AimPointComponent _aimPointComponent;
        [SerializeField]
        private float _cooldown;
        
        private float _timer;
        private ZenjectNetworkObjectSpawner _zenjectNetworkObjectSpawner;

        [Inject]
        public void Construct(ZenjectNetworkObjectSpawner zenjectNetworkObjectSpawner)
        {
            _zenjectNetworkObjectSpawner = zenjectNetworkObjectSpawner;
        }

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

        public void Shoot()
        {
            if (_timer <= 0)
            {
                _timer = _cooldown;
                Vector3 targetPoint = _aimPointComponent.GetAimPoint();
                // Вычисляем направление от дула оружия к нашей точке прицеливания
                Vector3 aimDirection = (targetPoint - _firePoint.position).normalized;
                ShootServerRpc(aimDirection);
            }
        }

        [ServerRpc]
        private void ShootServerRpc(Vector3 aimDirection)
        {
            // Debug.Log($"[SERVER] RPC получен! Текущий таймер: {_timer}");
            Vector3 position = _firePoint.position;
            Quaternion rotation = Quaternion.LookRotation(aimDirection);
            Projectile projectile = _zenjectNetworkObjectSpawner.SpawnPrefab(_projectilePrefab, position, rotation);
            projectile.Initialize(NetworkObjectId);
        }
    }
}
