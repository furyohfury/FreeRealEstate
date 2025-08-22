using Cysharp.Threading.Tasks;

namespace Game.BeatmapLaunch
{
	public interface IBeatmapLaunchable
	{
		UniTask Launch(BeatmapLaunchContext context);
	}
}