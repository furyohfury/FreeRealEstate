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
		private IDisposable _disposable;

		public EnterNamePresenter(EnterNameView enterNameView, PlayerNickname playerNickname)
		{
			_enterNameView = enterNameView;
			_playerNickname = playerNickname;
		}

		public void Initialize()
		{
			_enterNameView.Init();
			_disposable = _enterNameView.OnNicknameNameEntered
			                            .Subscribe(nickname => _playerNickname.Nickname = nickname);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}