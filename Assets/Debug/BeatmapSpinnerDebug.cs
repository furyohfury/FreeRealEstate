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
		[SerializeField]
		private DifficultyConfig _difficultyConfig;

		public IDifficulty GetDifficulty()
		{
			return _difficultyConfig;
		}

		public MapElement[] GetMapElements()
		{
			return _elements;
		}
	}
}