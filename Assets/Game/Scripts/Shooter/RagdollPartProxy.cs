using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class RagdollPartProxy : MonoBehaviour
    {
        public RagdollComponent RagdollComponent
        {
            get => _ragdollComponent;
            set => _ragdollComponent = value;
        }
        
        [SerializeField]
        private RagdollComponent _ragdollComponent;
    }
}
