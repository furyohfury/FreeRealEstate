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
		private readonly ConsumablesValuesConfig _valuesConfig;
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
			iConsume.OnConsumeEnd
			        .Subscribe(OnValueConsumed)
			        .AddTo(_disposable);
		}

		private void OnValueConsumed(GameObject entity)
		{
			if (entity.TryGetComponent(out IIdentifier iIdentifier) == false)
			{
				Debug.LogError("Consumed entity has no ID");
				return;
			}

			var id = iIdentifier.Id;
			if (_valuesConfig.Values.TryGetValue(id, out int points))
			{
				_shipPoints.Points.Value += points;
			}
			else
			{
				Debug.LogError("Consumed entity has no value in config");
			}
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}