using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class PauseEditorDebug : MonoBehaviour
    {
        private void Update()
        {
#if UNITY_EDITOR

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                EditorApplication.isPaused = true;
            }
#endif
        }
    }
}
