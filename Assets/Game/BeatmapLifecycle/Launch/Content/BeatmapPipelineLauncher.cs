using Cysharp.Threading.Tasks;
using Game.BeatmapControl;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapPipelineLauncher : IBeatmapLaunchable
	{
		private readonly BeatmapPipeline _beatmapPipeline;

		public BeatmapPipelineLauncher(BeatmapPipeline beatmapPipeline)
		{
			_beatmapPipeline = beatmapPipeline;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			_beatmapPipeline.SetMap(context.SelectedVariant.BeatmapConfig.Beatmap);
		}
	}
}