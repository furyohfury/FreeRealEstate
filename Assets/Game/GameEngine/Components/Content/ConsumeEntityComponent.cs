using System;
using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class ConsumeEntityComponent : IComponent
	{
		public Subject<int> OnValueConsumed = new();

		[SerializeField]
		private Transform _endPoint;
		[SerializeField]
		private float _animationDuration;

		[Button]
		public void ConsumeEntity(Entity entity)
		{
			if (entity.TryGetComponent(out MoveRigidbodyComponent moveComponent) == false)
			{
				throw new NullReferenceException("No move component on entity" + entity.gameObject.name);
			}

			if (entity.TryGetComponent(out DestroyComponent destroyComponent) == false)
			{
				throw new NullReferenceException("No destroy component on entity" + entity.gameObject.name);
			}

			if (entity.TryGetComponent(out CarriableComponent carriableComponent))
			{
				carriableComponent.ClearCarriers();
			}

			// moveComponent.SetKinematic(true);
			// DOTween.To(() => moveComponent.Position,
			// 	pos => moveComponent.MoveTo(pos),
			// 	_endPoint.position,
			// 	_animationDuration)
			entity.transform
			      .DOMove(_endPoint.position, _animationDuration)
			      .SetEase(Ease.InOutBounce)
			      .OnComplete(OnEntityConsumed(entity, destroyComponent));
		}

		private TweenCallback OnEntityConsumed(Entity entity, DestroyComponent destroyComponent)
		{
			return () =>
			{
				if (entity.TryGetComponent(out PointsValueComponent pointsValueComponent))
				{
					var componentValue = pointsValueComponent.Value;
					OnValueConsumed.OnNext(componentValue);
				}

				destroyComponent.Destroy();
			};
		}
	}
}