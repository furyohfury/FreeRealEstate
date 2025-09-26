using System;
using Game.SceneSwitch;
using R3;
using VContainer.Unity;

namespace Game.UI.Navigation
{
	public sealed class BackButtonPresenter : IStartable, IDisposable
	{
		private readonly IButtonView _backButton;
		private readonly ISceneSwitchable _sceneSwitchable;
		private IDisposable _disposable;

		public BackButtonPresenter(IButtonView backButton, ISceneSwitchable sceneSwitchable)
		{
			_backButton = backButton;
			_sceneSwitchable = sceneSwitchable;
		}

		public void Start()
		{
			_disposable = _backButton.OnButtonPressed
			                         .Subscribe(_ => _sceneSwitchable.SwitchScene());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}