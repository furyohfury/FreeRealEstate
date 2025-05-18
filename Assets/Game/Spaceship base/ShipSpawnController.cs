﻿using System;
using Cysharp.Threading.Tasks;
using GameEngine;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class ShipSpawnController : IInitializable, IDisposable
	{
		private readonly Entity _ship;
		private readonly ShipPoints _shipPoints;
		private readonly float _delayBetweenSpawns = 0.5f; // TODO to config
		private readonly CompositeDisposable _disposable = new();
		private ShipSpawnerComponent _shipSpawnerComponent;
		private bool _isSpawning;

		public ShipSpawnController(Entity ship, ShipPoints shipPoints)
		{
			_ship = ship;
			_shipPoints = shipPoints;
		}

		public void Initialize()
		{
			_shipSpawnerComponent = _ship.GetComponent<ShipSpawnerComponent>();

			_shipPoints.Points
			           .Pairwise()
			           .Where(pair => pair.Previous < pair.Current
			                          && _isSpawning == false)
			           .Subscribe(_ => OnPointGained().Forget())
			           .AddTo(_disposable);
		}

		private async UniTask OnPointGained()
		{
			_isSpawning = true;
			ReactiveProperty<int> points = _shipPoints.Points;
			while (points.Value > 0)
			{
				await SpawnEntity();
				points.Value -= 1; // TODO delete magic number 
			}

			_isSpawning = false;
		}

		private async UniTask SpawnEntity()
		{
			_shipSpawnerComponent.CreateEntity();
			await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenSpawns));
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}