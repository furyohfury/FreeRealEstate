using UnityEngine;
using UnityEngine.Serialization;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "Beatmap", menuName = "Beatmap/Beatmap")]
	public sealed class Beatmap : ScriptableObject, IBeatmap
	{
		public IDifficulty GetDifficulty()
		{
			return _difficultyConfig;
		}

		public MapElement[] GetMapElements()
		{
			return _mapElements;
		}

		[SerializeReference]
		private MapElement[] _mapElements;
		[FormerlySerializedAs("_difficulty")] [SerializeField]
		private DifficultyConfig _difficultyConfig;
	}
}