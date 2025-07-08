using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "ConsumablesValuesConfig", menuName = "Create config/ConsumablesValuesConfig")]
	public sealed class ConsumablesValuesConfig : SerializedScriptableObject
	{
		public IReadOnlyDictionary<string, int> Values => _values;
		
		[SerializeField]
		private Dictionary<string, int> _values;
	}
}