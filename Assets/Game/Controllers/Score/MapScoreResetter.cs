using System;
using Game.BeatmapControl;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class MapScoreResetter : IStartable, IDisposable
	{
		private readonly IMapScore _mapScore;
		private BeatmapPipeline _beatmapPipeline;
		private readonly CompositeDisposable _disposable = new();

		public MapScoreResetter(IMapScore mapScore, BeatmapPipeline beatmapPipeline)
		{
			_mapScore = mapScore;
			_beatmapPipeline = beatmapPipeline;
		}

		public void Start()
		{
			_beatmapPipeline.Map
			                .Subscribe(_ => _mapScore.Reset())
			                .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}