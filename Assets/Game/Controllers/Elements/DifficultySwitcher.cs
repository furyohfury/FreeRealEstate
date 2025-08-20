using System;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class DifficultySwitcher : IStartable, IDisposable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly ClickHandleResultStrategy _clickHandleResultStrategy;
		private readonly CompositeDisposable _disposable = new();

		public DifficultySwitcher(ClickHandleResultStrategy clickHandleResultStrategy, BeatmapPipeline beatmapPipeline)
		{
			_clickHandleResultStrategy = clickHandleResultStrategy;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Start()
		{
			_beatmapPipeline.Map
			                .Where(map => map != null)
			                .Subscribe(map => _clickHandleResultStrategy.SetDifficulty(map.GetDifficulty()))
			                .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}