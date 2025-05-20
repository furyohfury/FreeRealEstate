﻿using System;
using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class ConsumeEntityComponent
	{
		public Subject<GameObject> OnEntityConsumed = new();

		[SerializeField]
		private Transform _endPoint;
		[SerializeField]
		private float _animationDuration;

		[Button]
		public void ConsumeEntity(GameObject entity)
		{
			if (entity.TryGetComponent(out IMoveable moveable) == false)
			{
				throw new NullReferenceException("No move component on entity" + entity.gameObject.name);
			}

			if (entity.TryGetComponent(out IDestroyable destroyable) == false)
			{
				throw new NullReferenceException("No destroy component on entity" + entity.gameObject.name);
			}

			DOTween.To(
				       () => moveable.Position,
				       pos => moveable.MoveTo(pos),
				       _endPoint.position,
				       _animationDuration)
			       .SetEase(Ease.InOutBounce)
			       .OnComplete(OnConsumed(entity, destroyable));
		}

		private TweenCallback OnConsumed(GameObject entity, IDestroyable destroyable)
		{
			return () =>
			{
				OnEntityConsumed.OnNext(entity);
				destroyable.Destroy();
			};
		}
	}
}