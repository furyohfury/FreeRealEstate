using UnityEngine;
using UnityEngine.Serialization;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "Beatmap", menuName = "Beatmap/Beatmap")]
	public sealed class BeatmapConfig : ScriptableObject, IBeatmap
	{
		public int GetBpm()
		{
			return _bpm;
		}

		public IDifficulty GetDifficulty()
		{
			return _difficultyConfig;
		}

		public MapElement[] GetMapElements()
		{
			return _mapElements;
		}

		[SerializeField]
		private int _bpm = 120;
		[SerializeReference]
		private MapElement[] _mapElements;
		[FormerlySerializedAs("_difficulty")] [SerializeField]
		private DifficultyConfig _difficultyConfig;
	}
}