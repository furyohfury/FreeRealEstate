using System;
using R3;
using UnityEngine;

namespace Game.SongMapTime
{
	public sealed class BeatmapLauncher : IDisposable
	{
		private readonly IMapTime _mapTime;
		private readonly CompositeDisposable _disposable = new();

		public BeatmapLauncher(IMapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void LaunchActiveMap()
		{
			_disposable.Clear();
			Observable.EveryUpdate()
			          .Subscribe(_ => _mapTime.AddTime(Time.deltaTime))
			          .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}