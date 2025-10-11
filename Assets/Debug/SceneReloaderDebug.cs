using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Debug
{
	public class SceneReloaderDebug : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
			}
		}
	}
}