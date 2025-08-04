using UnityEngine;

namespace Game.Visuals
{
	[CreateAssetMenu(fileName = "DrumrollPrefabConfig", menuName = "Visuals/PrefabConfig/DrumrollPrefabConfig")]
	public sealed class DrumrollPrefabConfig : ScriptableObject
	{
		public string DrumrollPrefabId => _drumrollPrefabId;
		[SerializeField]
		private string _drumrollPrefabId;
	}
}