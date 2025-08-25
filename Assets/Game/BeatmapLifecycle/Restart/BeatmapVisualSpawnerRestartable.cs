using Game.Visuals;

namespace Game.BeatmapRestart
{
	public sealed class BeatmapVisualSpawnerRestartable : IBeatmapRestartable
	{
		private readonly ElementsVisualsSpawner _elementsVisualsSpawner;

		public BeatmapVisualSpawnerRestartable(ElementsVisualsSpawner elementsVisualsSpawner)
		{
			_elementsVisualsSpawner = elementsVisualsSpawner;
		}

		public void Restart()
		{
			_elementsVisualsSpawner.Reset();
		}
	}
}