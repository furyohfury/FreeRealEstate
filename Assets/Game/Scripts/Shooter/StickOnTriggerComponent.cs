using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class StickOnTriggerComponent : NetworkBehaviour
    {
        [SerializeField]
        private float _stickDistance = 0.1f;
        [SerializeField]
        private bool _isCollided;
        [SerializeField]
        private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false
                || _isCollided
                || other.TryGetComponent(out Player _))
            {
                return;
            }

            _isCollided = true;
            Vector3 stickPosition = transform.position + (other.transform.position - transform.position).normalized * _stickDistance;
            transform.position = stickPosition;
            transform.SetParent(other.transform);
            SpawnFakeArrowClientRpc(stickPosition, transform.rotation, other.transform); // todo передавать только номер кости и айди
                                                                                         // networkobject'a сам. У себя тоже можно разрушить че
                                                                                         // еще нужно дестроить с делеем
            NetworkObject.Despawn();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void SpawnFakeArrowClientRpc(Vector3 position, Quaternion rotation, Transform parent)
        {
            Instantiate(gameObject, position, rotation, parent);
        }
    }
}
