using TriInspector;
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
        private GameObject _fakeObject;

        [ShowInInspector]
        [ReadOnly]
        private bool _isCollided;

        public void OnTrigger(Collider other)
        {
            if (_isCollided
                || other.TryGetComponent(out Player _))
            {
                return;
            }

            _isCollided = true;

            if (other.TryGetComponent(out RagdollPartProxy ragdollPartProxy))
            {
                Vector3 stickPosition = transform.position + (other.transform.position - transform.position).normalized * _stickDistance;
                NetworkObject networkObject = ragdollPartProxy.RagdollComponent.NetworkObject;
                ulong networkObjectId = networkObject.NetworkObjectId;
                SpawnFakeArrowRpc(networkObjectId, other.gameObject.name, stickPosition,
                    transform.rotation); // todo передавать только номер кости и айди
            }
            else
            {
                Vector3 stickPosition = transform.position + transform.forward * _stickDistance;
                SpawnFakeArrowRpc(stickPosition, transform.rotation);
            }

            NetworkObject.Despawn();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void SpawnFakeArrowRpc(
            ulong targetNetworkObjectId,
            string boneName,
            Vector3 pos,
            Quaternion rot)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(targetNetworkObjectId, out var targetNetObj)
                && targetNetObj.TryGetComponent(out IRagdollComponent targetRagdollComponent))
            {
                Collider boneCollider = targetRagdollComponent.GetCollider(boneName);

                if (boneCollider == null)
                {
                    return;
                }

                Transform hitTransform = boneCollider.transform;
                Instantiate(_fakeObject, pos, rot, hitTransform);
                Debug.Log($"Spawned fake arrow on {boneName}");
            }
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void SpawnFakeArrowRpc(Vector3 pos, Quaternion rot)
        {
            Instantiate(_fakeObject, pos, rot);
            Debug.Log($"Spawned fake arrow in obstacle");
        }
    }
}
