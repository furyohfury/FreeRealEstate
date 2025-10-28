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
			Debug.Log($"is server = {NetworkManager.Singleton.IsServer}");
			Debug.Log($"is host = {NetworkManager.Singleton.IsHost}");
			Debug.Log($"is client = {NetworkManager.Singleton.IsClient}");
		}

		[Inject]
		private RoundRestarter _roundRestarter;
	}
}