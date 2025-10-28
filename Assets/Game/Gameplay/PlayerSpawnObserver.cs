using System;
using Game.Network;
using Gameplay;
using R3;
using Zenject;

namespace Game
{
	public sealed class PlayerSpawnObserver : IInitializable, IDisposable
	{
		private readonly HostPlayerService _hostPlayerService;
		private readonly ClientPlayerService _clientPlayerService;
		private readonly PlayerSpawner _playerSpawner;
		private readonly CompositeDisposable _disposable = new();

		public PlayerSpawnObserver(HostPlayerService hostPlayerService, ClientPlayerService clientPlayerService, PlayerSpawner playerSpawner)
		{
			_hostPlayerService = hostPlayerService;
			_clientPlayerService = clientPlayerService;
			_playerSpawner = playerSpawner;
		}

		public void Initialize()
		{
			_playerSpawner.OnHostSpawned
			              .Subscribe(player =>
			              {
				              _hostPlayerService.HostPlayer = player;
			              })
			              .AddTo(_disposable);

			_playerSpawner.OnClientSpawned
			              .Subscribe(player => _clientPlayerService.ClientPlayer = player)
			              .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}