using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.BeatmapLaunch;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapRestarter
	{
		private readonly IEnumerable<IBeatmapRestartable> _restartables;
		private readonly CurrentBundleService _bundleService;
		private readonly BeatmapLauncher _launcher;

		public BeatmapRestarter(
			IEnumerable<IBeatmapRestartable> restartables,
			BeatmapLauncher launcher,
			CurrentBundleService bundleService
		)
		{
			_restartables = restartables;
			_launcher = launcher;
			_bundleService = bundleService;
		}

		public void Restart()
		{
			foreach (var restartable in _restartables)
			{
				restartable.Restart();
			}

			_launcher.Launch(_bundleService.CurrentBundle, _bundleService.CurrentVariant).Forget();
		}
	}
}