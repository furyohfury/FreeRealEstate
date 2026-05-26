using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class RagdollComponent : NetworkBehaviour
    {
        public Collider[] Colliders
        {
            get => _colliders;
            set => _colliders = value;
        }
        
        [SerializeField]
        private Collider[] _colliders;
        private readonly Dictionary<string, Collider> _colliderDict = new Dictionary<string, Collider>();

        private void Awake()
        {
            for (int i = 0, count = Colliders.Length; i < count; i++)
            {
                _colliderDict.Add(Colliders[i].gameObject.name, Colliders[i]);
            }
        }

        public Collider GetCollider(string colliderName)
        {
            return _colliderDict.GetValueOrDefault(colliderName);
        }
    }
}
