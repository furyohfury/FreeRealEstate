using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public sealed class EditorOnlyFinderDebug : MonoBehaviour
	{
		[Button]
		private void LogEditorOnlyObjects()
		{
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("EditorOnly");
			for (int i = 0, count = gameObjects.Length; i < count; i++)
			{
				Debug.Log(gameObjects[i].name);
			}
		}
	}
}