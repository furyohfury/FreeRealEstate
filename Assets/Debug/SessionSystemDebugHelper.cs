using System;
using Game.App;
using Game.Network;
using Sirenix.OdinInspector;
using Unity.Netcode;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameDebug
{
	public sealed class SessionSystemDebugHelper : MonoBehaviour
	{
		[Inject]
		private SessionSystem _sessionSystem;
		[Inject]
		private PlayerNickname _playerNickname;

		[Button]
		private void StartAsHost(string name)
		{
			_sessionSystem.HostPrivateSession(name);
		}

		[Button]
		private async void JoinSessionByCode(string code)
		{
			await _sessionSystem.JoinSessionByCode(code, _playerNickname.Nickname);
		}

		[Button]
		private void SwitchToGameplay()
		{
			NetworkManager.Singleton.SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
		}
	}
}