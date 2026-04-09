using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class DebugManager : Singleton<DebugManager>
    {
        [SerializeField]
        private GameObject[] _debugObjects;

#if !UNITY_EDITOR
        protected override void Awake()
        {
            base.Awake();

            for (int index = 0; index < _debugObjects.Length; index++)
                {
                    GameObject go = _debugObjects[index];
                    go.SetActive(false);
                }
        }
#endif

#if !UNITY_ANDROID
        private void Update()
        {
            if (Keyboard.current.leftCtrlKey.isPressed
                && Keyboard.current.leftAltKey.isPressed
                && Keyboard.current.cKey.wasPressedThisFrame)
            {
                for (int index = 0; index < _debugObjects.Length; index++)
                {
                    GameObject go = _debugObjects[index];
                    go.SetActive(!go.activeSelf);
                }
            }
        }
#endif
    }
}
