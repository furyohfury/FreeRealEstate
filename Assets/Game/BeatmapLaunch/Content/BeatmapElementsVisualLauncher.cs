using Cysharp.Threading.Tasks;
using Game.Visuals;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapElementsVisualLauncher : IBeatmapLaunchable
	{
		private readonly ElementsVisualsSpawner _elementsVisualsSpawner;

		public BeatmapElementsVisualLauncher(ElementsVisualsSpawner elementsVisualsSpawner)
		{
			_elementsVisualsSpawner = elementsVisualsSpawner;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			_elementsVisualsSpawner.LaunchMap(context.SelectedVariant.BeatmapConfig.Beatmap);
		}
	}
}