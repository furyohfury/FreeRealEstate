using System;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class MapChangeObserver : IStartable, IDisposable
	{
		private readonly ActiveMapService _activeMapService;
		private readonly ActiveElementService _activeElementService;
		private readonly SerialDisposable _disposable = new();

		public MapChangeObserver(ActiveMapService activeMapService, ActiveElementService activeElementService)
		{
			_activeMapService = activeMapService;
			_activeElementService = activeElementService;
		}

		public void Start()
		{
			_disposable.Disposable = _activeMapService.ActiveMap
			                                          .Where(map => map != null)
			                                          .Subscribe(map => _activeElementService.SetElement(map.GetMapElements()[0]));
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}