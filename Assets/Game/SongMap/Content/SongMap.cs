using UnityEngine;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "SongMap", menuName = "SongMap/SongMap")]
	public sealed class SongMap : ScriptableObject, ISongMap
	{
		public IDifficulty GetDifficulty()
		{
			return _difficulty;
		}

		public MapElement[] GetMapElements()
		{
			return _mapElements;
		}

		[SerializeReference]
		private MapElement[] _mapElements;
		[SerializeField]
		private Difficulty _difficulty;
	}
}