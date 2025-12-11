using System;
using Game.Network;
using R3;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public sealed class PlayerDisconnectObserver : IInitializable, IDisposable
	{
		private readonly GameFinisher _gameFinisher;
		private readonly MyPlayerService _myPlayerService;
		private SessionSystem _sessionSystem;
		
		private bool _isGameFinished;
		private readonly CompositeDisposable _disposable = new CompositeDisposable();

		public PlayerDisconnectObserver(GameFinisher gameFinisher, MyPlayerService myPlayerService, SessionSystem sessionSystem)
		{
			_gameFinisher = gameFinisher;
			_myPlayerService = myPlayerService;
			_sessionSystem = sessionSystem;
		}

		public void Initialize()
		{
			Observable.FromEvent<ulong>(h => NetworkManager.Singleton.OnClientDisconnectCallback += h,
				          h =>
				          {
					          if (NetworkManager.Singleton != null)
					          {
						          NetworkManager.Singleton.OnClientDisconnectCallback -= h;
					          }
				          })
			          .Subscribe(OnClientDisconnectCallback)
			          .AddTo(_disposable);

			_gameFinisher.OnPlayerWon
			             .Subscribe(_ => _isGameFinished = true)
			             .AddTo(_disposable);
		}

		private void OnClientDisconnectCallback(ulong clientId)
		{
			bool hasClientDisconnected = HasClientDisconnected(clientId);
			bool hasHostDisconnected = HasHostDisconnected(clientId);
			Debug.Log($"Client {clientId} disconnected!. hasClientDisconnected =  {hasClientDisconnected}, hasHostDisconnected = {hasHostDisconnected}");
			
			if (hasClientDisconnected
			    || hasHostDisconnected)
			{
				_gameFinisher.FinishGameByPlayerWon(_myPlayerService.MyPlayer);
			}
		}

		private bool HasHostDisconnected(ulong clientId) // was kicked by host when he was leaving
		{
			return NetworkManager.Singleton.IsHost == false
			       && !_isGameFinished
			       && clientId == NetworkManager.Singleton.LocalClient.ClientId;
		}
		
		private bool HasClientDisconnected(ulong clientId) // TODO засчитывает победу хосту в консоли
		{
			return NetworkManager.Singleton.IsHost
			       && !_isGameFinished
			       && clientId != NetworkManager.Singleton.LocalClient.ClientId
			       && _sessionSystem.IsLeaving == false;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}
