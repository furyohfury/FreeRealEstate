using System;
using Game.BeatmapRestart;
using R3;
using VContainer.Unity;

namespace Game.UI.Navigation
{
	public sealed class RestartButtonPresenter : IStartable, IDisposable
	{
		private readonly IButtonView _backButton;
		private readonly BeatmapRestarter _beatmapRestarter;
		private IDisposable _disposable;

		public RestartButtonPresenter(IButtonView backButton, BeatmapRestarter beatmapRestarter)
		{
			_backButton = backButton;
			_beatmapRestarter = beatmapRestarter;
		}

		public void Start()
		{
			_disposable = _backButton.OnButtonPressed
			                         .Subscribe(_ => _beatmapRestarter.Restart());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}