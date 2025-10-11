using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Debug
{
	public class StartSessionUI : MonoBehaviour
	{
		public Button StartHost;
		public Button StartClient;

		private void OnEnable()
		{
			StartHost.onClick.AddListener(OnStartHost);
			StartClient.onClick.AddListener(OnStartClient);
		}

		private void OnStartHost()
		{
			NetworkManager.Singleton.StartHost();
		}

		private void OnStartClient()
		{
			NetworkManager.Singleton.StartClient();
		}

		private void OnDisable()
		{
			StartHost.onClick.RemoveListener(OnStartHost);
			StartClient.onClick.RemoveListener(OnStartClient);
		}
	}
}