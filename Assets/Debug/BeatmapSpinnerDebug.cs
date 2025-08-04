using Beatmaps;
using UnityEngine;

namespace GameDebug
{
	[CreateAssetMenu(fileName = "BeatmapSpinnerDebug", menuName = "Debug/BeatmapSpinnerDebug")]
	public sealed class BeatmapSpinnerDebug : ScriptableObject, IBeatmap
	{
		private readonly MapElement[] _elements = {
			                                          new SingleNote(4
				                                          , Notes.Red),
			                                          new Spinner(5
				                                          , 3),
			                                          new SingleNote(20
				                                          , Notes.Red),
		                                          };
		public int GetBpm()
		{
			return 60;
		}

		public IDifficulty GetDifficulty()
		{
			return new Difficulty("Normal", new IDifficultyParams[]
			                                {
				                                new SingleNoteClickIntervalParams(0.03f), new SpinnerClicksPerSecondParams(2)
				                                , new DrumrollClickIntervalParams(0.04f)
			                                });
		}

		public MapElement[] GetMapElements()
		{
			return _elements;
		}
	}
}