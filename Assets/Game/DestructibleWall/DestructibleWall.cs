using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	public sealed class DestructibleWall :
		MonoBehaviour,
		IHitPoints,
		ITakeDamage,
		IDestroyable,
		IIdentifier,
		IPikminInteractable
	{
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

		[SerializeField] [Header("VFX")]
		private DestroyVFXComponent _destroyVFXComponent;

		private readonly CompositeDisposable _disposable = new();

		private void Awake()
		{
			_lifeComponent.CurrentHealth
			              .Where(hp => hp < _lifeComponent.MaxHealth * 0.6f)
			              .Take(1)
			              .Subscribe(_ =>
			              {
				              _destroyVFXComponent.SpawnVFX();
				              _stateChangeComponent.ChangeState();
			              })
			              .AddTo(_disposable);

			_lifeComponent.CurrentHealth
			              .Where(hp => hp < _lifeComponent.MaxHealth * 0.3f)
			              .Take(1)
			              .Subscribe(_ =>
			              {
				              _destroyVFXComponent.SpawnVFX();
				              _stateChangeComponent.ChangeState();
			              })
			              .AddTo(_disposable);
		}

		public void TakeDamage(int delta)
		{
			_lifeComponent.ChangeHealth(delta);
			if (_lifeComponent.IsAlive == false)
			{
				Destroy();
			}
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}
	}
}