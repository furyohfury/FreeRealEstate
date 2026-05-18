using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class LifetimeComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _lifeTime = 10f;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Destroy(gameObject, _lifeTime);
            }
        }
    }
}
