using System;
using TriInspector;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [Serializable]
    public sealed class MoveComponent
    {
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        [SerializeField]
        private CharacterController _characterController;
        [field: SerializeField]
        private float _speed = 5f;
        [SerializeField]
        private float _gravity = -9.81f;
        [ShowInInspector]
        [ReadOnly]
        private Vector3 _direction;

        public void Update()
        {
            var isGrounded = _characterController.isGrounded;

            if (isGrounded && Direction.y < 0)
            {
                var vector3 = Direction;
                vector3.y = -2f;
                Direction = vector3;
            }

            _direction.y += _gravity * Time.deltaTime;

            Vector3 direction = _characterController.transform.TransformDirection(Direction);
            _characterController.Move(direction * (_speed * Time.deltaTime));
        }
    }
}
