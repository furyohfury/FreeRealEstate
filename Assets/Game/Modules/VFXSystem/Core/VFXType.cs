using System;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace VFX
{
	[Serializable]
	public sealed class VFXType
	{
		public string ID => _id;
#if ODIN_INSPECTOR
		[ValueDropdown(nameof(GetAllVfxTypes))]
#endif
		[SerializeField]
		private string _id;

		public override string ToString()
		{
			return _id;
		}

		private static IEnumerable<string> GetAllVfxTypes()
		{
			var typesAsset = Resources.Load<VFXTypesDatabase>("VFXTypesDatabase");
			if (typesAsset == null)
			{
				Debug.Log("Create VFXTypesDatabase asset to make VFX types");
			}
			else if (typesAsset.Types == null || typesAsset.Types.Length <= 0)
			{
				Debug.Log("No VFX types in database yet");
			}

			return typesAsset?.Types ?? Array.Empty<string>();
		}
	}
}