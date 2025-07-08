using System;
using R3;
using R3.Triggers;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class ObjectDetectorComponent
	{
		public Subject<GameObject> OnEntityDetected = new();

		[SerializeField]
		private Collider _detectionZone;
		[SerializeField]
		private LayerMask _targetLayer;
		private CompositeDisposable _disposable = new();

		public void Initialize()
		{
			_detectionZone.OnTriggerEnterAsObservable()
			              .Where(other => (1 << other.gameObject.layer & _targetLayer.value) != 0)
			              .Subscribe(OnDetected)
			              .AddTo(_disposable);
		}

		public void OnDetected(Collider other)
		{
			OnEntityDetected.OnNext(other.gameObject);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}