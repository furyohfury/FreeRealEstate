using System;
using Unity.Netcode;
using Zenject;

namespace Gameplay
{
	public sealed class MyPlayerController : IInitializable, IDisposable
	{
		private readonly MyPlayerService _myPlayerService;

		public MyPlayerController(MyPlayerService myPlayerService)
		{
			_myPlayerService = myPlayerService;
		}

		public void Initialize()
		{
			NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
		}

		private void OnClientConnectedCallback(ulong obj)
		{
			_myPlayerService.MyPlayer = NetworkManager.Singleton.IsHost
				? Player.One
				: Player.Two;
		}

		public void Dispose()
		{
			if (NetworkManager.Singleton != null)
			{
				NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
			}
		}
	}
}