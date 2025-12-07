using System;
using R3;
using Unity.Netcode;
using Zenject;

namespace Gameplay
{
	public sealed class PlayerDisconnectObserver : IInitializable, IDisposable
	{
		private readonly GameFinisher _gameFinisher;
		private readonly MyPlayerService _myPlayerService;
		private bool _isGameFinished;
		private readonly CompositeDisposable _disposable = new();

		public PlayerDisconnectObserver(GameFinisher gameFinisher, MyPlayerService myPlayerService)
		{
			_gameFinisher = gameFinisher;
			_myPlayerService = myPlayerService;
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
			if (!_isGameFinished &&
			    clientId != NetworkManager.Singleton.LocalClient.ClientId) // TODO сначала кикает другого а потом дисконектается лол
			{
				_gameFinisher.FinishGameByPlayerWon(_myPlayerService.MyPlayer);
			}
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}