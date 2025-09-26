using Cysharp.Threading.Tasks;
using Game.Visuals;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapBackgroundLauncher : IBeatmapLaunchable
	{
		private readonly IBackgroundChanger _backgroundChanger;

		public BeatmapBackgroundLauncher(IBackgroundChanger backgroundChanger)
		{
			_backgroundChanger = backgroundChanger;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			var background = context.Bundle.Background;
			_backgroundChanger.Change(background);
		}
	}
}