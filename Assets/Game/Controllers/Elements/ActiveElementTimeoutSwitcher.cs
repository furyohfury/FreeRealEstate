using System;
using System.Linq;
using Beatmaps;
using Game.SongMapTime;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ActiveElementTimeoutSwitcher : IStartable, ITickable, IDisposable
	{
		private readonly ActiveElementSwitcher _switcher;
		private readonly ActiveElementService _activeElementService;
		private readonly ActiveMapService _activeMapService;
		private readonly IMapTime _mapTime;
		private float _clickInterval;

		public ActiveElementTimeoutSwitcher(ActiveElementSwitcher switcher, ActiveElementService activeElementService
			, ActiveMapService activeMapService, IMapTime mapTime)
		{
			_switcher = switcher;
			_activeElementService = activeElementService;
			_activeMapService = activeMapService;
			_mapTime = mapTime;
		}

		private readonly SerialDisposable _disposable = new();

		public void Start()
		{
			_disposable.Disposable = _activeMapService.ActiveMap
			                                          .Where(map => map != null)
			                                          .Subscribe(map =>
			                                          {
				                                          _clickInterval = map
				                                                           .GetDifficulty()
				                                                           .GetDifficultyParams()
				                                                           .OfType<ClickIntervalParams>()
				                                                           .Single()
				                                                           .GetClickInterval();
			                                          });
		}

		public void Tick()
		{
			var element = _activeElementService.Element.CurrentValue;
			if (element == null)
			{
				return;
			}

			var elementTimeSeconds = element.TimeSeconds;
			var mapTime = _mapTime.GetMapTimeInSeconds();
			if (IsBeyondClickInterval(mapTime, elementTimeSeconds))
			{
				_switcher.SetNextElement();
			}
		}

		private bool IsBeyondClickInterval(float mapTime, float elementTimeSeconds)
		{
			return mapTime - elementTimeSeconds > _clickInterval;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}