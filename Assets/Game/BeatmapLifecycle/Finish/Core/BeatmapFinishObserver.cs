using System;
using Game.BeatmapLaunch;
using R3;
using VContainer.Unity;

namespace Game.BeatmapFinish
{
	public sealed class BeatmapFinishObserver : IInitializable, IDisposable
	{
		private readonly IBeatmapEndObservable _beatmapEndObservable;
		private readonly BeatmapFinisher _beatmapFinisher;
		private IDisposable _disposable;

		public BeatmapFinishObserver(IBeatmapEndObservable beatmapEndObservable, BeatmapFinisher beatmapFinisher)
		{
			_beatmapEndObservable = beatmapEndObservable;
			_beatmapFinisher = beatmapFinisher;
		}

		public void Initialize()
		{
			_disposable = _beatmapEndObservable.OnCurrentMapEnded
			                                   .Subscribe(_ => OnBeatmapEnded());
		}

		private void OnBeatmapEnded()
		{
			_beatmapFinisher.Finish();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}