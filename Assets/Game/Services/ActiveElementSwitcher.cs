namespace Game
{
	public sealed class ActiveElementSwitcher
	{
		private readonly ActiveElementIndexService _activeElementIndexService;
		private readonly ActiveElementService _activeElementService;
		private readonly ActiveMapService _activeMapService;

		public ActiveElementSwitcher(
			ActiveElementService activeElementService,
			ActiveMapService activeMapService,
			ActiveElementIndexService activeElementIndexService
		)
		{
			_activeElementService = activeElementService;
			_activeMapService = activeMapService;
			_activeElementIndexService = activeElementIndexService;
		}

		public void SetNextElement()
		{
			var map = _activeMapService.ActiveMap.CurrentValue;
			var elements = map.GetMapElements();
			var nextIndex = _activeElementIndexService.ActiveIndex + 1;
			if (nextIndex < elements.Length)
			{
				_activeElementService.SetElement(elements[nextIndex]);
			}
		}
	}
}