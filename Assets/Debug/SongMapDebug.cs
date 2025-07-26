using Beatmaps;
using UnityEngine;

namespace GameDebug
{
	[CreateAssetMenu(fileName = "SongMapDebug", menuName = "Debug/SongMapDebug")]
	public sealed class SongMapDebug : ScriptableObject, ISongMap
	{
		[SerializeField]
		private Difficulty _difficulty;
		private MapElement[] _elements = new MapElement[]
		                                 {
			                                 new SingleNote(4, Notes.Blue),
			                                 new SingleNote(5, Notes.Red),
			                                 new SingleNote(6, Notes.Blue),
			                                 new SingleNote(7, Notes.Red),
			                                 new SingleNote(8, Notes.Blue),
			                                 new SingleNote(11, Notes.Red),
		                                 };
		
		public IDifficulty GetDifficulty()
		{
			return _difficulty;
		}

		public MapElement[] GetMapElements()
		{
			return _elements;
		}
	}
}