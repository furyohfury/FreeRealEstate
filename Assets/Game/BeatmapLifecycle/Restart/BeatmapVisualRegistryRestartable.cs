using System.Linq;
using Game.Visuals;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapVisualRegistryRestartable : IBeatmapRestartable
	{
		private readonly ElementViewsRegistry _elementViewsRegistry;
		private readonly IElementViewDestroyer _elementViewDestroyer;

		public BeatmapVisualRegistryRestartable(
			ElementViewsRegistry elementViewsRegistry,
			IElementViewDestroyer elementViewDestroyer
		)
		{
			_elementViewsRegistry = elementViewsRegistry;
			_elementViewDestroyer = elementViewDestroyer;
		}

		public void Restart()
		{
			foreach (var element in _elementViewsRegistry.Registry.Keys.ToArray())
			{
				_elementViewDestroyer.DestroyView(element);
			}
		}
	}
}