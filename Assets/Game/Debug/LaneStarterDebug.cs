using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class LaneStarterDebug : MonoBehaviour
    {
        [SerializeField]
        private bool _launchOnStart = true;

        private void Start()
        {
            if (_launchOnStart)
                StartSpawn();
        }

        [Button]
        public void StartSpawn()
        {
            SessionLauncher.Instance.LaunchSession();
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                StartSpawn();
            }
        }
    }
}
