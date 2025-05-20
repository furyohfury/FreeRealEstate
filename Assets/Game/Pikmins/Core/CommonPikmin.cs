using System;
using GameEngine;
using UnityEngine;

namespace Game
{
	public sealed class CommonPikmin :  // TODO bind navmesh and animation with isalive too
		MonoBehaviour,
		IChangeHealth,
		ICarry,
		IAttackable
	{
		public int MaxHealth => _lifeComponent.MaxHealth;
		public int CurrentHealth => _lifeComponent.CurrentHealth;
		
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

		private void AnimateMovement()
		{
			var isMoving = _navMeshComponent.Velocity != Vector3.zero;
			_animatorComponent.Animator.SetBool(AnimatorHash.IsMoving, isMoving);
		}

		public void ChangeHealth(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public bool TryCarry(GameObject entity)
		{
			 return _carryComponent.TryCarry(entity);
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