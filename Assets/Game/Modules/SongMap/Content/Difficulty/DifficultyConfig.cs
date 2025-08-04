using UnityEngine;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "Difficulty", menuName = "Beatmap/Difficulty")]
	public sealed class DifficultyConfig : ScriptableObject, IDifficulty
	{
		[SerializeField]
		private Difficulty _difficulty;

		public string GetName()
		{
			return _difficulty.GetName();
		}

		public IDifficultyParams[] GetDifficultyParams()
		{
			return _difficulty.GetDifficultyParams();
		}
	}
}