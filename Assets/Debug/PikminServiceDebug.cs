using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public class PikminServiceDebug : MonoBehaviour
	{
		private PikminService _pikminService;

		[ShowInInspector]
		private int _count => _pikminService.CurrentCount;

		[ShowInInspector]
		private int maxCount => PikminService.CurrentMaxCount;

		[Inject]
		public void Construct(PikminService pikminService)
		{
			_pikminService = pikminService;
		}
	}
}