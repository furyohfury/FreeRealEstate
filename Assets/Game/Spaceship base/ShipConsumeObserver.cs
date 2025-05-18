using System;
using GameEngine;
using R3;
using Zenject;

namespace Game
{
	public sealed class ShipConsumeObserver : IInitializable, IDisposable
	{
		private readonly Entity _ship;
		private readonly ShipPoints _shipPoints;
		private ConsumeEntityComponent _consumeEntityComponent;
		private readonly CompositeDisposable _disposable = new();

		public ShipConsumeObserver(Entity ship, ShipPoints shipPoints)
		{
			_ship = ship;
			_shipPoints = shipPoints;
		}

		public void Initialize()
		{
			_consumeEntityComponent = _ship.GetComponent<ConsumeEntityComponent>();
			_consumeEntityComponent.OnValueConsumed
			                       .Subscribe(OnValueConsumed)
			                       .AddTo(_disposable);
		}

		private void OnValueConsumed(int points)
		{
			_shipPoints.Points.Value += points;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}