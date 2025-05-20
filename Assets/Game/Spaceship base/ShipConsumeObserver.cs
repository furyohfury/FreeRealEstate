using System;
using GameEngine;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class ShipConsumeObserver : IInitializable, IDisposable
	{
		private readonly Ship _ship;
		private readonly ShipPoints _shipPoints;
		private ConsumablesValuesConfig _valuesConfig;
		private readonly CompositeDisposable _disposable = new();

		public ShipConsumeObserver(Ship ship, ShipPoints shipPoints, ConsumablesValuesConfig valuesConfig)
		{
			_ship = ship;
			_shipPoints = shipPoints;
			_valuesConfig = valuesConfig;
		}

		public void Initialize()
		{
			var iConsume = _ship.GetComponent<IConsume>();
			iConsume.OnEntityConsumed
			                       .Subscribe(OnValueConsumed)
			                       .AddTo(_disposable);
		}

		private void OnValueConsumed(GameObject entity)
		{
			if (entity.TryGetComponent(out IIdentifier iIdentifier) == false)
			{
				return;
			}

			var id = iIdentifier.Id;
			_shipPoints.Points.Value += _valuesConfig.Values[id];
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}