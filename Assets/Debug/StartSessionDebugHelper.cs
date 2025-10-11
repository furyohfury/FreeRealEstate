using System;
using Unity.Netcode;
using UnityEngine;

namespace Debug
{
	public class StartSessionDebugHelper : MonoBehaviour
	{
		public bool CreateOnStart = true;
		public NetworkManager NetworkManager;

		private void Start()
		{
			if (CreateOnStart)
			{
				NetworkManager.StartHost();
			}
		}
	}
}