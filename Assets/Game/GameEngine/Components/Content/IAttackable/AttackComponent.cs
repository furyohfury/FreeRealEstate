using System;
using Cysharp.Threading.Tasks;
using Game;
using R3;
using R3.Triggers;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class AttackComponent // TODO separate on objects (not components)
	{
		public AndCondition CanAttack => _canAttack;
		private AndCondition _canAttack = new();

		[SerializeField]
		private Collider _weaponCollider;
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private AnimatorEventReceiver _animatorEventReceiver;
		[Header("Parameters")] [SerializeField]
		private int _damage;
		[SerializeField]
		private float _cooldown;

		private bool _cooldownPassed = true;
		private int _attackHash = AnimatorHash.Attack;
		private CompositeDisposable _disposable = new();

		public void Initialize()
		{
			_weaponCollider.OnTriggerEnterAsObservable()
			       .Subscribe(OnAttacked)
			       .AddTo(_disposable);

			_animatorEventReceiver.SubscribeOnEvent(AnimatorEvents.ATTACK_STARTED, OnAttackStarted);
			_animatorEventReceiver.SubscribeOnEvent(AnimatorEvents.ATTACK_FINISHED, OnAttackFinished);

			CanAttack.AddCondition(() => _cooldownPassed);
		}

		public void Attack()
		{
			if (CanAttack.Invoke() == false)
			{
				return;
			}

			AnimateAttack();
			SetCooldown().Forget();
		}

		private void OnAttackStarted()
		{
			_weaponCollider.enabled = true;
		}

		private void OnAttackFinished()
		{
			_weaponCollider.enabled = false;
		}

		private void AnimateAttack()
		{
			_animator.SetTrigger(_attackHash);
		}

		private async UniTask SetCooldown()
		{
			_cooldownPassed = false;
			await UniTask.Delay(TimeSpan.FromSeconds(_cooldown));
			_cooldownPassed = true;
		}

		private void OnAttacked(Collider other)
		{
			if (other.gameObject.TryGetComponent(out ITakeDamage liveable))
			{
				liveable.TakeDamage(-_damage);
			}
		}

		public void Dispose()
		{
			_animatorEventReceiver.UnsubscribeOnEvent(AnimatorEvents.ATTACK_STARTED, OnAttackStarted);
			_animatorEventReceiver.UnsubscribeOnEvent(AnimatorEvents.ATTACK_FINISHED, OnAttackFinished);
			_disposable.Dispose();
		}
	}
}