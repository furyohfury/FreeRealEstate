using System;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class ActiveElementIndexService : IStartable, IDisposable
	{
		public int ActiveIndex => _activeIndex;

		private readonly ActiveElementService _activeElementService;
		private readonly ActiveMapService _activeMapService;
		private int _activeIndex = -1;
		private readonly CompositeDisposable _disposable = new();

		public ActiveElementIndexService(ActiveElementService activeElementService, ActiveMapService activeMapService)
		{
			_activeElementService = activeElementService;
			_activeMapService = activeMapService;
		}

		public void Start()
		{
			_activeElementService.Element
			                     .Subscribe(_ => _activeIndex++)
			                     .AddTo(_disposable);

			_activeMapService.ActiveMap
			                 .Subscribe(_ => Reset())
			                 .AddTo(_disposable);
		}

		private void Reset()
		{
			_activeIndex = -1;
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}