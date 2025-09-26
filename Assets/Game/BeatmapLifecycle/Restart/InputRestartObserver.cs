using System;
using Game.BeatmapRestart;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class InputRestartObserver : IStartable, IDisposable
	{
		private readonly IInputReader _inputReader;
		private readonly BeatmapRestarter _beatmapRestarter;
		private IDisposable _disposable;

		public InputRestartObserver(IInputReader inputReader, BeatmapRestarter beatmapRestarter)
		{
			_inputReader = inputReader;
			_beatmapRestarter = beatmapRestarter;
		}

		public void Start()
		{
			_disposable = _inputReader.OnRestart
			                          .Subscribe(_ => _beatmapRestarter.Restart());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}