using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Beatmaps
{
	[CreateAssetMenu(fileName = "Difficulty", menuName = "Beatmap/DifficultyConfig")]
	public sealed class DifficultyConfig : ScriptableObject
	{
		public Difficulty Difficulty => _difficulty;

		[SerializeField]
		private Difficulty _difficulty;

		[Button]
		private void AddSingleNoteClickInterval(float interval)
		{
			List<IDifficultyParams> difficultyParamsList = _difficulty.DifficultyParameters.ToList();
			difficultyParamsList.Add(new SingleNoteClickIntervalParams(interval));
			_difficulty.DifficultyParameters = difficultyParamsList.ToArray();
		}

		[Button]
		private void AddSpinnerClicksPerSecond(float count)
		{
			List<IDifficultyParams> difficultyParamsList = _difficulty.DifficultyParameters.ToList();
			difficultyParamsList.Add(new SpinnerClicksPerSecondParams(count));
			_difficulty.DifficultyParameters = difficultyParamsList.ToArray();
		}

		[Button]
		private void AddDrumrollClickInterval(float interval)
		{
			List<IDifficultyParams> difficultyParamsList = _difficulty.DifficultyParameters.ToList();
			difficultyParamsList.Add(new DrumrollClickIntervalParams(interval));
			_difficulty.DifficultyParameters = difficultyParamsList.ToArray();
		}
	}
}