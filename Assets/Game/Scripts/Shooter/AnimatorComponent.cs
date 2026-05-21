using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class AnimatorComponent : NetworkBehaviour
    {
        [SerializeField]
        private Animator _animator;
        private static readonly int _isMoving = Animator.StringToHash("IsMoving");
        private static readonly int _shoot = Animator.StringToHash("Shoot");

        public void SetIsMoving(bool isMoving)
        {
            if (IsOwner)
            {
                _animator.SetBool(_isMoving, isMoving);
            }
        }

        public void PlayShootAnim()
        {
            if (IsOwner)
            {
                _animator.SetTrigger(_shoot);
            }
        }
    }
}
