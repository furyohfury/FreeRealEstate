using System;
using Game.Network;
using R3;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using Zenject;

namespace Gameplay
{
	public sealed class MyPlayerController : IInitializable, IDisposable
	{
		private readonly MyPlayerService _myPlayerService;
		private readonly SessionSystem _sessionSystem;
		private IDisposable _disposable;

		public MyPlayerController(MyPlayerService myPlayerService, SessionSystem sessionSystem)
		{
			_myPlayerService = myPlayerService;
			_sessionSystem = sessionSystem;
		}

		public void Initialize()
		{
			_disposable = _sessionSystem.OnSessionStarted
			                            .Subscribe(OnClientConnectedCallback);
		}

		private void OnClientConnectedCallback(ISession _)
		{
			_myPlayerService.MyPlayer = NetworkManager.Singleton.IsHost
				? Player.One
				: Player.Two;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}