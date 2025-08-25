using Game.BeatmapBundles;

namespace Game.BeatmapLaunch
{
	public sealed class CurrentBundleService
	{
		public BeatmapBundle CurrentBundle { get; set; }
		public BeatmapVariant CurrentVariant { get; set; }
	}
}