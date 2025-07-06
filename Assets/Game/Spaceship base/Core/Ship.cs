using System.Collections.Generic;
using GameEngine;
using R3;
using UnityEngine;

namespace Game
{
	public sealed class Ship : MonoBehaviour,
		IConsume,
		ISpawner,
		IObjectDetector
	{
		public Subject<GameObject> OnEntityDetected => _objectDetectorComponent.OnEntityDetected;
		public Observable<GameObject> OnConsumeEnd => _consumeEntityComponent.OnConsumeEnd;
		public Observable<GameObject> OnConsumeStart => _consumeEntityComponent.OnConsumeStart;

		[SerializeField]
		private ConsumeEntityComponent _consumeEntityComponent;
		[SerializeField]
		private ShipSpawnerComponent _shipSpawnerComponent;
		[SerializeField]
		private ObjectDetectorComponent _objectDetectorComponent;
		[Header("Effects")]
		[SerializeField]
		private ConsumeEntityEffectComponent _consumeEntityEffectComponent;

		private readonly HashSet<GameObject> _detectedEntities = new();
		private readonly CompositeDisposable _disposable = new();

		private void Start()
		{
			_objectDetectorComponent.Initialize();

			_objectDetectorComponent.OnEntityDetected
			                        .Subscribe(OnObjectDetected)
			                        .AddTo(_disposable);

			_consumeEntityComponent.OnConsumeStart
			                       .Subscribe(_ => _consumeEntityEffectComponent.PlayEffect())
			                       .AddTo(_disposable);
		}

		private void OnObjectDetected(GameObject obj)
		{
			if (_detectedEntities.Add(obj) == false)
			{
				return;
			}

			_consumeEntityComponent.ConsumeEntity(obj);
		}

		public void ConsumeEntity(GameObject entity)
		{
			_consumeEntityComponent.ConsumeEntity(entity);
		}

		public GameObject CreateEntity()
		{
			return _shipSpawnerComponent.CreateEntity();
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
			_objectDetectorComponent.Dispose();
		}
	}
}