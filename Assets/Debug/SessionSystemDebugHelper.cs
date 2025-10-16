using Game.Network;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameDebug
{
	public sealed class SessionSystemDebugHelper : MonoBehaviour
	{
		[Inject]
		private SessionSystem _sessionSystem;

		[Button]
		private void StartAsHost(string name)
		{
			_sessionSystem.StartSessionAsHost(name);
		}

		[Button]
		private async void JoinSessionByCode(string code)
		{
			await _sessionSystem.JoinSessionByCode(code);
		}

		[Button]
		private void SwitchToGameplay()
		{
			NetworkManager.Singleton.SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
		}
	}
}