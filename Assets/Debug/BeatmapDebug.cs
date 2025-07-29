using Beatmaps;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameDebug
{
	[CreateAssetMenu(fileName = "SongMapDebug", menuName = "Debug/SongMapDebug")]
	public sealed class BeatmapDebug : ScriptableObject, IBeatmap
	{
		[FormerlySerializedAs("_difficulty")] [SerializeField]
		private DifficultyConfig _difficultyConfig;
		private readonly MapElement[] _elements = new MapElement[]
		                                          {
			                                          new SingleNote(4, Notes.Blue), new SingleNote(5, Notes.Red), new SingleNote(6, Notes.Blue)
			                                          , new SingleNote(7, Notes.Red), new SingleNote(8, Notes.Blue), new SingleNote(11, Notes.Red)
		                                          };

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