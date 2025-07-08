using System;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	[SelectionBase]
	public sealed class DestructibleWall :
		MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		IDestroyable,
		IIdentifier,
		IPikminInteractable
	{
		public Observable<GameObject> OnDead => _destroyComponent.OnDead;
		public string Id => _idComponent.ID;
		public int HitPoints => _lifeComponent.CurrentHealth.CurrentValue;

		[SerializeField]
		private DestroyComponent _destroyComponent;
		[SerializeField]
		private IdComponent _idComponent;
		[SerializeField]
		private LifeComponent _lifeComponent;

		[SerializeField] [Header("Visual")]
		private TakeDamageStateChangeComponent _stateChangeComponent;
		
		[SerializeField] [Header("UI")]
		private HealthbarUIComponent _healthbarUI;

		[SerializeField] [Header("Effects")]
		private DestroyVFXComponent _destroyVFXComponent;
		[SerializeField]
		private DestroySFXComponent _destroySfxComponent;
		[SerializeField]
		private StateChangeVFXComponent _stateChangeVFXComponent;
		[SerializeField]
		private float[] _effectPlayHpThresholds;

		private readonly CompositeDisposable _disposable = new();

		private void Start()
		{
			if (_effectPlayHpThresholds != null)
			{
				for (int i = 0, count = _effectPlayHpThresholds.Length; i < count; i++)
				{
					var index = i;
					_lifeComponent.CurrentHealth
					              .Where(hp => hp <= _lifeComponent.MaxHealth * _effectPlayHpThresholds[index])
					              .Take(1)
					              .Subscribe(_ =>
					              {
						              _stateChangeVFXComponent.PlayVFX();
						              _destroySfxComponent.PlaySFX();
						              _stateChangeComponent.ChangeState();
					              })
					              .AddTo(_disposable);
				}
			}

			_lifeComponent.CurrentHealth
			              .Subscribe(hp =>
			              {
				              _healthbarUI.SetRatio((float)hp / _lifeComponent.MaxHealth);
				              if (hp <= 0)
				              {
					              _healthbarUI.Hide();
				              }
			              })
			              .AddTo(_disposable);
			
			_lifeComponent.CurrentHealth
			              .Where(hp => hp <= 0)
			              .Take(1)
			              .Subscribe(_ =>
			              {
				              _destroyVFXComponent.PlayVFX();
				              _destroyComponent.Destroy();
			              })
			              .AddTo(_disposable);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}
	}
}