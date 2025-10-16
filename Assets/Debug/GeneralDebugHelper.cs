using Gameplay;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace GameDebug
{
	public sealed class GeneralDebugHelper : MonoBehaviour
	{
		[Button]
		private void Print()
		{
			UnityEngine.Debug.Log($"is server = {NetworkManager.Singleton.IsServer}");
			UnityEngine.Debug.Log($"is host = {NetworkManager.Singleton.IsHost}");
			UnityEngine.Debug.Log($"is client = {NetworkManager.Singleton.IsClient}");
		}

		[Inject]
		private RoundRestarter _roundRestarter;

		[Button]
		private void RestartInHostSide()
		{
			_roundRestarter.RestartInHostSide();
		}
	}
}