using UnityEngine;

namespace VFX
{
	[CreateAssetMenu(fileName = "VFXTypesDatabase", menuName = "VFX system/VFXTypesDatabase")]
	public sealed class VFXTypesDatabase : ScriptableObject
	{
		public string[] Types;
	}
}