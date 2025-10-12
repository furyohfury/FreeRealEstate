using Sirenix.OdinInspector;
using UnityEngine;

namespace Debug
{
	public sealed class ScaleDebugHelper : MonoBehaviour
	{
		[SerializeField]
		private Transform[] _transforms;
		[SerializeField]
		private float _mult;

		[Button]
		private void Descale()
		{
			foreach (var t in _transforms)
			{
				t.localScale /= _mult;
			}
		}
		
		[Button]
		private void Scale()
		{
			foreach (var t in _transforms)
			{
				t.localScale *= _mult;
			}
		}
	}
}