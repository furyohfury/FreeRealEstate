using System;
using Game.ElementHandle;
using R3;
using VContainer.Unity;

namespace Game
{
	public sealed class DifficultySwitcher : IStartable, IDisposable
	{
		private readonly ActiveMapService _activeMapService;
		private readonly ElementsClickHandler _elementsClickHandler;
		private readonly CompositeDisposable _disposable = new();

		public DifficultySwitcher(ActiveMapService activeMapService, ElementsClickHandler elementsClickHandler)
		{
			_activeMapService = activeMapService;
			_elementsClickHandler = elementsClickHandler;
		}

		public void Start()
		{
			_activeMapService.ActiveMap
			                 .Where(map => map != null)
			                 .Subscribe(map => _elementsClickHandler.SetDifficulty(map.GetDifficulty()))
			                 .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}