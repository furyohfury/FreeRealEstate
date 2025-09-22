using System;
using Game.BeatmapControl;
using Game.BeatmapRestart;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapEndObservable : IBeatmapEndObservable, IInitializable, IDisposable, IBeatmapRestartable
	{
		public Observable<Unit> OnCurrentMapEnded => _onCurrentMapEnded;
		private readonly Subject<Unit> _onCurrentMapEnded = new();
		private readonly BeatmapPipeline _beatmapPipeline;

		private readonly SerialDisposable _disposable = new();

		public BeatmapEndObservable(BeatmapPipeline beatmapPipeline)
		{
			_beatmapPipeline = beatmapPipeline;
		}

		public void Initialize()
		{
			InitSubscription();
		}

		public void Restart()
		{
			InitSubscription();
		}

		private void InitSubscription()
		{
			_disposable.Disposable = Observable.EveryUpdate()
			                                   .Where(_ => _beatmapPipeline.IsEnded)
			                                   .Take(1)
			                                   .Subscribe(_ =>
			                                   {
				                                   _onCurrentMapEnded.OnNext(Unit.Default);
				                                   Debug.Log("OnMapEnded");
			                                   });
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}