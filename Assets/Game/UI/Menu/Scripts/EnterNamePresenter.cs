using System;
using Game.App;
using R3;
using Zenject;

namespace Game.UI
{
	public sealed class EnterNamePresenter : IInitializable, IDisposable
	{
		private readonly EnterNameView _enterNameView;
		private readonly PlayerNickname _playerNickname;
		private readonly GameModeView _gameModeView;
		private IDisposable _disposable;

		public EnterNamePresenter(EnterNameView enterNameView, PlayerNickname playerNickname, GameModeView gameModeView)
		{
			_enterNameView = enterNameView;
			_playerNickname = playerNickname;
			_gameModeView = gameModeView;
		}

		public void Initialize()
		{
			_enterNameView.Init();
			_disposable = _enterNameView.OnNicknameNameEntered
			                            .Subscribe(OnEnterPressed);
		}

		private void OnEnterPressed(string nickname)
		{
			_playerNickname.Nickname = nickname;
			_gameModeView.Show();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}