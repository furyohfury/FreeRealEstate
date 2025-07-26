using UnityEngine;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "Difficulty", menuName = "SongMap/Difficulty")]
	public sealed class Difficulty : ScriptableObject, IDifficulty
	{
		[SerializeField] 
		private string _name;
		[SerializeReference]
		private IDifficultyParams[] _difficultyParams;

		public string GetName()
		{
			return _name;
		}

		public IDifficultyParams[] GetDifficultyParams()
		{
			return _difficultyParams;
		}
	}
}