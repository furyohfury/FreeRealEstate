using System.Linq;

namespace Beatmaps
{
	public static class DifficultyExtensions
	{
		public static float GetSingleNoteClickInterval(this IDifficulty difficulty)
		{
			return difficulty
			       .GetDifficultyParams()
			       .OfType<SingleNoteClickIntervalParams>()
			       .Single()
			       .GetClickInterval();
		}

		public static float GetDrumrollClickInterval(this IDifficulty difficulty)
		{
			return difficulty
			       .GetDifficultyParams()
			       .OfType<DrumrollClickIntervalParams>()
			       .Single()
			       .GetClickInterval();
		}

		public static float GetSpinnerClicksPerSecond(this IDifficulty difficulty)
		{
			return difficulty
			       .GetDifficultyParams()
			       .OfType<SpinnerClicksPerSecondParams>()
			       .Single()
			       .GetClicksPerSecond();
		}
	}
}