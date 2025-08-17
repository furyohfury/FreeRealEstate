using System;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.BeatmapTime
{
	public sealed class MapTimeController : IInitializable, IDisposable
	{
		private readonly MapTime _mapTime;
		private IDisposable _disposable;

		public MapTimeController(MapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void Initialize()
		{
			_disposable = Observable.EveryUpdate(UnityFrameProvider.PreUpdate)
			                        .Subscribe(_ => _mapTime.Tick(Time.deltaTime));
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}