using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public class Player : NetworkBehaviour
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

        public override void OnNetworkSpawn()
        {
            _playerUIComponent.Init(_healthComponent.Health);
        }

        public void TakeDamage(float damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        public void SetDirection(Vector3 direction)
        {
            _moveComponent.Direction = direction;
        }

        public void SetRotationDirection(Vector3 direction)
        {
            _rotationComponent.Direction = direction;
        }

        private void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
            _animatorComponent.SetIsMoving(_moveComponent.Direction.x != 0 || _moveComponent.Direction.z != 0);
        }

        public override void OnNetworkDespawn()
        {
            _playerUIComponent.Dispose();
        }
    }
}
