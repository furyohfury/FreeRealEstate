using Beatmaps;
using UnityEngine;

namespace GameDebug
{
	[CreateAssetMenu(fileName = "BeatmapDrumrollDebug", menuName = "Debug/BeatmapDrumrollDebug")]
	public sealed class BeatmapDrumrollDebug : ScriptableObject, IBeatmap
	{
		private readonly MapElement[] _elements =
		{
			new SingleNote(4
				, Notes.Red)
			, new Drumroll(5
				, 3)
			, new SingleNote(15
				, Notes.Red)
		};

		public int GetBpm()
		{
			return 120;
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