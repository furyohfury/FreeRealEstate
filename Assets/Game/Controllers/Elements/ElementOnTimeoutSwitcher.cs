using System;
using Game.BeatmapControl;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ElementOnTimeoutSwitcher : IInitializable, IDisposable
	{
		private readonly BeatmapPipeline _beatmapPipeline;
		private readonly IHandleResultObservable _resultObservable;
		private IDisposable _disposable;

		public ElementOnTimeoutSwitcher(BeatmapPipeline beatmapPipeline, IHandleResultObservable resultObservable)
		{
			_beatmapPipeline = beatmapPipeline;
			_resultObservable = resultObservable;
		}

		public void Initialize()
		{
			_disposable = _resultObservable.OnElementHandled
			                               .OfType<HandleResult, MissHandleResult>()
			                               .Subscribe(_ => _beatmapPipeline.SwitchToNextElement());
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}