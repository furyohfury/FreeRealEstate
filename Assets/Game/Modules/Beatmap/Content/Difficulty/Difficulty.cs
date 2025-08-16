using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class Difficulty : IDifficulty
	{
		public string Name
		{
			get => _name;
			set => _name = value;
		}
		public IDifficultyParams[] DifficultyParameters
		{
			get => _difficultyParameters;
			set => _difficultyParameters = value;
		}

		[SerializeField]
		private string _name;
		[SerializeReference]
		private IDifficultyParams[] _difficultyParameters;

		public Difficulty(string name, IDifficultyParams[] difficultyParameters)
		{
			_name = name;
			_difficultyParameters = difficultyParameters;
		}

		public string GetName()
		{
			return _name;
		}

		public IDifficultyParams[] GetDifficultyParams()
		{
			return _difficultyParameters;
		}
	}
}