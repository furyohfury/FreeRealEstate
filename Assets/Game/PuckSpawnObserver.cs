using System;
using Game.Network;
using R3;
using Zenject;

namespace Game
{
	public sealed class PuckSpawnObserver : IInitializable, IDisposable
	{
		private readonly PuckSpawner _puckSpawner;
		private readonly PuckService _puckService;
		private IDisposable _disposable;

		public PuckSpawnObserver(PuckSpawner puckSpawner, PuckService puckService)
		{
			_puckSpawner = puckSpawner;
			_puckService = puckService;
		}

		public void Initialize()
		{
			_disposable = _puckSpawner.OnPuckSpawned
			                          .Subscribe(puck => _puckService.Puck = puck);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}