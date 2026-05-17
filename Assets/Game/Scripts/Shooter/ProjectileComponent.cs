using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class ProjectileComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _speed;

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }
}
