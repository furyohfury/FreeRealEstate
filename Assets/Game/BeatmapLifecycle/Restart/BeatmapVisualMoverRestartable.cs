using Game.Visuals;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapVisualMoverRestartable : IBeatmapRestartable
	{
		private readonly ElementViewsRegistry _elementViewsRegistry;
		private readonly ElementsHorizontalMover _elementsHorizontalMover;

		public BeatmapVisualMoverRestartable(
			ElementViewsRegistry elementViewsRegistry,
			ElementsHorizontalMover elementsHorizontalMover
		)
		{
			_elementViewsRegistry = elementViewsRegistry;
			_elementsHorizontalMover = elementsHorizontalMover;
		}

		public void Restart()
		{
			foreach (var element in _elementViewsRegistry.Registry.Keys)
			{
				_elementsHorizontalMover.Remove(element);
			}
		}
	}
}