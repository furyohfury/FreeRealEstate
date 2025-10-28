using System;
using Unity.Netcode;
using Zenject;

namespace Game.Network
{
	public sealed class LobbyLaunchSub : IInitializable, IDisposable
	{
		private readonly SessionSystem _sessionSystem;

		public LobbyLaunchSub(SessionSystem sessionSystem)
		{
			_sessionSystem = sessionSystem;
		}

		public void Initialize()
		{
			NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
		}

		private void OnClientConnectedCallback(ulong id)
		{
			if (id != NetworkManager.Singleton.LocalClientId)
			{
				_sessionSystem.SwitchScene(Scenes.GameplayScene);
			}
		}

		public void Dispose()
		{
			if (NetworkManager.Singleton != null)
			{
				NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
			}
		}
	}
}