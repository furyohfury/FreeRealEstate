using Game.ElementHandle;
using UnityEngine;

namespace Game.Services
{
	public sealed class DrumrollTickrateService
	{
		private readonly DrumrollTickRateConfig _tickRateConfig;

		public DrumrollTickrateService(DrumrollTickRateConfig tickRateConfig)
		{
			_tickRateConfig = tickRateConfig;
		}

		public float GetTickrate(int bpm)
		{
			return _tickRateConfig.GetTickRate(bpm);
		}

		public int GetNotesCountByDuration(int bpm, float drumrollDuration)
		{
			var tickRate = GetTickrate(bpm);
			var beatDuration = (float)60 / bpm;
			var tickInterval = beatDuration / tickRate;
			var noteCount = drumrollDuration / tickInterval;
			if (noteCount % 1 != 0)
			{
				Debug.LogWarning($"Drumroll duration must be dividable by {tickInterval}");
			}

			var drumRollNoteCount = Mathf.FloorToInt(noteCount);
			return drumRollNoteCount;
		}
	}
}