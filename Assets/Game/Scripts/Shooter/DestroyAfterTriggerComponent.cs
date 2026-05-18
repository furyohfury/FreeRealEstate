using System;
using Cysharp.Threading.Tasks;
using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class DestroyAfterTriggerComponent : NetworkBehaviour
    {
        [SerializeField] [RequiredGet]
        private NetworkObject _networkObject;
        [SerializeField]
        private float _delay;
        private bool _collided;

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer == false || _collided)
            {
                return;
            }

            _collided = true;
            DestroyWithDelayAsync().Forget();
        }

        private async UniTask DestroyWithDelayAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: gameObject.GetCancellationTokenOnDestroy());

            _networkObject.Despawn();
        }
    }
}
