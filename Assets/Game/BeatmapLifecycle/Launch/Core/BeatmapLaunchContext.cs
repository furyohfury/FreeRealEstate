using Game.BeatmapBundles;

namespace Game.BeatmapLaunch
{
	public class BeatmapLaunchContext
	{
		public BeatmapBundle Bundle => _bundle;
		public BeatmapVariant SelectedVariant => _selectedVariant;
		private readonly BeatmapVariant _selectedVariant;
		private readonly BeatmapBundle _bundle;

		public BeatmapLaunchContext(BeatmapBundle bundle, BeatmapVariant selectedVariant)
		{
			_bundle = bundle;
			_selectedVariant = selectedVariant;
		}
	}
}