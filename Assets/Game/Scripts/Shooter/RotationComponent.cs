using System;
using TriInspector;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [Serializable]
    public sealed class RotationComponent
    {
        public Vector3 Direction { get; set; }
        [SerializeField]
        private float _rotationSpeed = 3f;
        [SerializeField]
        [RequiredGet]
        private Transform _transform;

        public void Update()
        {
            Quaternion rotation = Quaternion.Lerp(_transform.rotation, _transform.rotation * Quaternion.Euler(Direction),
                Time.deltaTime * _rotationSpeed);
            _transform.rotation = rotation;
        }
    }
}
