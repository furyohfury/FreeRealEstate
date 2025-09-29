using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Game.BeatmapLaunch
{
	public sealed class SceneStartMapLauncher : IPostStartable
	{
		private readonly BeatmapLauncher _beatmapLauncher;
		private readonly CurrentBundleService _currentBundleService;

		public SceneStartMapLauncher(BeatmapLauncher beatmapLauncher, CurrentBundleService currentBundleService)
		{
			_beatmapLauncher = beatmapLauncher;
			_currentBundleService = currentBundleService;
		}

		public void PostStart()
		{
			if (_currentBundleService.HasActiveBeatmap)
			{
				_beatmapLauncher.Launch(_currentBundleService.CurrentBundle, _currentBundleService.CurrentVariant).Forget();
			}
		}
	}
}