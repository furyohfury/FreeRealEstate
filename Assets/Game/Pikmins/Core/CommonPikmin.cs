using System;
using GameEngine;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class CommonPikmin :  // TODO bind navmesh and animation with isalive too
		MonoBehaviour,
		ITakeDamage,
		ICarry,
		IAttackable,
		IPikminTarget
	{
		public int CurrentHealth => _lifeComponent.CurrentHealth.CurrentValue;

		[ShowInInspector]
		public GameObject Target => _target;

		[SerializeField]
		private LifeComponent _lifeComponent;

		[SerializeField]
		private CarryComponent _carryComponent;

		[SerializeField]
		private AttackComponent _attackComponent;

		[SerializeField]
		private NavMeshComponent _navMeshComponent;

		[SerializeField]
		private AnimatorComponent _animatorComponent;

		private GameObject _target;

		private void Awake()
		{
			_attackComponent.CanAttack.AddCondition(() => _lifeComponent.IsAlive);
		}

		private void Start()
		{
			_attackComponent.Initialize();
		}

		private void Update()
		{
			AnimateMovement();
		}

		public bool TrySetTarget(GameObject target)
		{
			if (target.TryGetComponent(out IPikminInteractable _))
			{
				_target = target;
				return true;
			}

			return false;
		}

		private void AnimateMovement()
		{
			var isMoving = _navMeshComponent.Velocity != Vector3.zero;
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public bool TryCarry(GameObject entity)
		{
			 return _carryComponent.TryCarry(entity);
		}

		public void StopCarry()
		{
			_carryComponent.StopCarry();
		}

		public void Attack()
		{
			_attackComponent.Attack();
		}

		private void OnDestroy()
		{
			_attackComponent.Dispose();
		}
	}
}