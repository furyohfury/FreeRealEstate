using Gameplay;
using UnityEngine;
using Zenject;

namespace GameDebug
{
	public sealed class PlayerDebugHelper : MonoBehaviour
	{
		[Inject]
		private MyPlayerService _myPlayerService;

		private void Start()
		{
			Debug.Log($"My player is {_myPlayerService.MyPlayer.ToString()}");
		}
	}
}