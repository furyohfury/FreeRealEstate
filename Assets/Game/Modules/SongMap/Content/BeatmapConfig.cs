using Sirenix.OdinInspector;
using UnityEngine;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "BeatmapConfig", menuName = "Beatmap/BeatmapConfig")]
	public sealed class BeatmapConfig : ScriptableObject
	{
		public Beatmap Beatmap => _beatmap;

		[SerializeField]
		private Beatmap _beatmap;

		[Button]
		private void SetDifficulty(DifficultyConfig difficultyConfig)
		{
			_beatmap.Difficulty = difficultyConfig.Difficulty;
		}
	}
}