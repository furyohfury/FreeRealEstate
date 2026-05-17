using System;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    [Serializable]
    public sealed class AnimatorComponent
    {
        [SerializeField]
        private Animator _animator;
        private static readonly int _isMoving = Animator.StringToHash("IsMoving");
        private static readonly int _shoot = Animator.StringToHash("Shoot");

        public void SetIsMoving(bool isMoving)
        {
            _animator.SetBool(_isMoving, isMoving);
        }

        public void PlayShootAnim()
        {
            _animator.SetTrigger(_shoot);
        }
    }
}
