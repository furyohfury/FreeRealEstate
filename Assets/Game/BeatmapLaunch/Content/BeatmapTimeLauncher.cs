using Cysharp.Threading.Tasks;
using Game.BeatmapLaunch;

namespace Game.BeatmapTime
{
	public sealed class BeatmapTimeLauncher : IBeatmapLaunchable
	{
		private readonly MapTime _mapTime;

		public BeatmapTimeLauncher(MapTime mapTime)
		{
			_mapTime = mapTime;
		}

		public void Launch()
		{
			_mapTime.Launch();
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			_mapTime.Launch();
		}
	}
}