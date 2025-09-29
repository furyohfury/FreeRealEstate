using Game.BeatmapBundles;

namespace Game.BeatmapLaunch
{
	public sealed class CurrentBundleService
	{
		public bool HasActiveBeatmap => CurrentBundle != null && CurrentVariant != null;
		public BeatmapBundle CurrentBundle { get; set; }
		public BeatmapVariant CurrentVariant { get; set; }
	}
}