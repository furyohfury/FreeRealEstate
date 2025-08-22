using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.BeatmapBundles;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapLauncher
	{
		private readonly IEnumerable<IBeatmapLaunchable> _launchables;

		public BeatmapLauncher(IEnumerable<IBeatmapLaunchable> launchables)
		{
			_launchables = launchables;
		}

		public async UniTask Launch(BeatmapBundle bundle, int variantIndex)
		{
			var variant = bundle.BeatmapsVariants[variantIndex];
			var launchContext = new BeatmapLaunchContext(bundle, variant);
			var tasks = new List<UniTask>();
			foreach (var launchable in _launchables)
			{
				var task = launchable.Launch(launchContext);
				tasks.Add(task);
			}

			await UniTask.WhenAll(tasks);
		}
	}
}