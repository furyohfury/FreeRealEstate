using Cysharp.Threading.Tasks;
using Game.App;
using Game.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameDebug
{
	public class StartSessionDebugUI : MonoBehaviour
	{
		public Button StartHost;
		public TMP_InputField hostCode;
		public Button StartClient;
		public TMP_InputField clientCode;
		public Button LeaveGameButton;

		[Inject]
		private SessionSystem _sessionSystem;
		[Inject]
		private PlayerNickname _playerNickname;

		private void OnEnable()
		{
			StartHost.onClick.AddListener(OnStartHost);
			StartClient.onClick.AddListener(OnStartClient);
			LeaveGameButton.onClick.AddListener(OnLeaveGame);
		}

		private void OnLeaveGame()
		{
			_sessionSystem.LeaveCurrentSession().Forget();
		}

		private async void OnStartHost()
		{
			await _sessionSystem.HostPublicSession(_playerNickname.Nickname);
			hostCode.text = _sessionSystem.ActiveSession.Code;
		}

		private void OnStartClient()
		{
			_sessionSystem.JoinSessionByCode(clientCode.text, _playerNickname.Nickname).Forget();
		}

		private void OnDisable()
		{
			StartHost.onClick.RemoveListener(OnStartHost);
			StartClient.onClick.RemoveListener(OnStartClient);
			LeaveGameButton.onClick.RemoveListener(OnLeaveGame);
		}
	}
}