using System;
using System.Collections.Generic;
using GameEngine;
using R3;
using Zenject;

namespace Game
{
	public sealed class ShipDetectObjectObserver : IInitializable, IDisposable
	{
		private readonly Entity _ship;
		private ConsumeEntityComponent _consumeEntityComponent;

		private readonly HashSet<Entity> _detectedEntities = new();
		private readonly CompositeDisposable _disposable = new();

		public ShipDetectObjectObserver(Entity ship)
		{
			_ship = ship;
		}

		public void Initialize()
		{
			_consumeEntityComponent = _ship.GetComponent<ConsumeEntityComponent>();

			var objectsDetectorComponent = _ship.GetComponent<ObjectsDetectorComponent>();
			objectsDetectorComponent.OnEntityDetected
			                        .Subscribe(OnEntityDetected)
			                        .AddTo(_disposable);
		}

		private void OnEntityDetected(Entity entity)
		{
			if (_detectedEntities.Add(entity) == false)
			{
				return;
			}

			_consumeEntityComponent.ConsumeEntity(entity);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}