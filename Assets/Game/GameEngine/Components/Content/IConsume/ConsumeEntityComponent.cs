using System;
using DG.Tweening;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class ConsumeEntityComponent
	{
		public Observable<GameObject> OnConsumeEnd => _onConsumeEnd;
		public Observable<GameObject> OnConsumeStart => _onConsumeStart;
		private Subject<GameObject> _onConsumeStart = new();
		private Subject<GameObject> _onConsumeEnd = new();

		[SerializeField]
		private Transform _endPoint;
		[SerializeField]
		private float _animationDuration;

		[Button]
		public void ConsumeEntity(GameObject entity)
		{
			if (entity.TryGetComponent(out IDestroyable destroyable) == false)
			{
				throw new NullReferenceException("No destroy component on entity" + entity.gameObject.name);
			}

			_onConsumeStart.OnNext(entity);

			entity.transform
			      .DOMove(_endPoint.position, _animationDuration)
			      .SetEase(Ease.InOutBounce)
			      .OnComplete(() =>
			      {
				      _onConsumeEnd.OnNext(entity);
				      destroyable.Destroy();
			      });
		}
	}
}