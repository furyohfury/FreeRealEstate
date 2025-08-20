using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.ElementHandle
{
	[CreateAssetMenu(fileName = "DrumrollTickrateConfig", menuName = "Beatmap/Elements/DrumrollTickrateConfig")]
	public sealed class DrumrollTickRateConfig : SerializedScriptableObject
	{
		[SerializeField]
		private Dictionary<int, int> _tickRateByBpm;

		private const float DEFAULT_TICK_RATE = 8f;

		public float GetTickRate(int bpm)
		{
			var sortedBpms = _tickRateByBpm.Keys.OrderBy(key => key);
			foreach (var bpmKey in sortedBpms)
			{
				if (bpm <= bpmKey)
				{
					return _tickRateByBpm[bpmKey];
				}
			}

			return DEFAULT_TICK_RATE;
		}
	}
}