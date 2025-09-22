using Cysharp.Threading.Tasks;
using Game.UI;

namespace Game.BeatmapLaunch
{
	public class WindowsBeatmapLaunchable : IBeatmapLaunchable
	{
		private readonly IWindowSystem _windowSystem;

		public WindowsBeatmapLaunchable(IWindowSystem windowSystem)
		{
			_windowSystem = windowSystem;
		}

		public async UniTask Launch(BeatmapLaunchContext _)
		{
			_windowSystem.CloseAll();
		}
	}
}