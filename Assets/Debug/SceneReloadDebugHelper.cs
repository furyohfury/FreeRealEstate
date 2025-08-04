using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GameDebug
{
	public class SceneReloadDebugHelper : MonoBehaviour
	{
		private void Update()
		{
			if (Keyboard.current.rKey.wasPressedThisFrame)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}