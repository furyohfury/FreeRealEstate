using Game.BeatmapControl;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapPipelineRestartable : IBeatmapRestartable
	{
		private readonly BeatmapPipeline _beatmapPipeline;

		public BeatmapPipelineRestartable(BeatmapPipeline beatmapPipeline)
		{
			_beatmapPipeline = beatmapPipeline;
		}

		public void Restart()
		{
			_beatmapPipeline.RestartMap();
		}
	}
}