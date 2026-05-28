using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public class Player : NetworkBehaviour, IHealth, IRagdollComponent
    {
        public NetworkVariable<float> MaxHealth => _healthComponent.MaxHealth;
        public NetworkVariable<float> Health => _healthComponent.Health;
        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private RotationComponent _rotationComponent;
        [SerializeField]
        private AnimatorComponent _animatorComponent;
        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private PlayerUIComponent _playerUIComponent;
        [SerializeField]
        private ShootComponent _shootComponent;
        [SerializeField]
        private CameraComponent _cameraComponent;
        [SerializeField]
        private AimUIComponent _aimUIComponent;
        [SerializeField]
        private RagdollComponent _ragdollComponent;
        [SerializeField]
        private SpawnPositionComponent _spawnPositionComponent;

        public override void OnNetworkSpawn()
        {
            _playerUIComponent.Init(_healthComponent.Health);
        }

        public void TakeDamage(float damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        public float GetCurrentHealth()
        {
            return _healthComponent.Health.Value;
        }

        public void SetDirection(Vector3 direction)
        {
            _moveComponent.Direction = direction;
        }

        public void SetRotationDirection(Vector3 direction)
        {
            _rotationComponent.Direction = direction;
        }

        public void Shoot()
        {
            if (_cameraComponent.IsAiming)
            {
                _animatorComponent.PlayShootAnim();
                _shootComponent.Shoot();
                Debug.Log($"<color=green>Shoot</color>");
            }
        }

        public void Aim()
        {
            _cameraComponent.SetShootingPreset();
            _aimUIComponent.ShowAimUI();
        }

        public void CancelAim()
        {
            _cameraComponent.SetWalkingPreset();
            _aimUIComponent.HideAimUI();
        }

        public Collider GetCollider(string boneId)
        {
            return _ragdollComponent.GetCollider(boneId);
        }

        private void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
            _animatorComponent.SetIsMoving(_moveComponent.Direction.x != 0 || _moveComponent.Direction.z != 0);
        }

        public void GetToSpawnPosition()
        {
            _spawnPositionComponent.GetToSpawnPositionRpc();
        }

        public override void OnNetworkDespawn()
        {
            _playerUIComponent.Dispose();
        }
    }
}
