using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class Difficulty : IDifficulty
	{
		[SerializeField]
		private string _name;
		[SerializeReference]
		private IDifficultyParams[] _difficultyParams;

		public Difficulty(string name, IDifficultyParams[] difficultyParams)
		{
			_name = name;
			_difficultyParams = difficultyParams;
		}

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