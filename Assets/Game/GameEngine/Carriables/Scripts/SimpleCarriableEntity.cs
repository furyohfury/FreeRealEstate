using System;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	[SelectionBase]
	public sealed class SimpleCarriableEntity : MonoBehaviour,
		IGetAnchorPoint,
		ICarriable,
		IIdentifier,
		IMoveable,
		IPikminInteractable,
		IDestroyable
	{
		public Observable<GameObject> OnDead => _destroyComponent.OnDead;
		public string Id => _idComponent.ID;
		public bool IsCarried => _carriableComponent.IsCarried;
		public Vector3 Position => transform.position;

		[SerializeField]
		private AnchorPointsComponent _anchorPointsComponent;
		[SerializeField]
		private CarriableComponent _carriableComponent;
		[SerializeField]
		private DestroyComponent _destroyComponent;
		[SerializeField]
		private IdComponent _idComponent;
		[SerializeField]
		private MoveTransformComponent _moveTransformComponent;
		[SerializeField][Header("UI")]
		private CarriableUIComponent _carriableUIComponent;

		private readonly CompositeDisposable _disposable = new();

		private void Start()
		{
			_carriableComponent.CurrentForce
			                   .Subscribe(force =>
				                   _carriableUIComponent.SetAmount(
					                   force,
					                   _carriableComponent.Weight)
			                   )
			                   .AddTo(_disposable);
		}

		private void Update()
		{
			_carriableComponent.Update(Time.deltaTime);
		}

		public Transform GetAnchorPoint()
		{
			return _anchorPointsComponent.GetAnchorPoint();
		}

		public bool AddCarrier(Transform carrier, int force)
		{
			return _carriableComponent.AddCarrier(carrier, force);
		}

		public void RemoveCarrier(Transform carrier, int force)
		{
			_carriableComponent.RemoveCarrier(carrier, force);
		}

		public void ClearCarriers()
		{
			_carriableComponent.ClearCarriers();
		}

		public void Move(Vector3 direction)
		{
			_moveTransformComponent.Move(direction);
		}

		public void MoveTo(Vector3 position)
		{
			_moveTransformComponent.MoveTo(position);
		}

		public void Destroy()
		{
			_destroyComponent.Destroy();
		}

		private void OnDestroy()
		{
			_carriableComponent.ClearCarriers();
			_disposable.Dispose();
		}
	}
}