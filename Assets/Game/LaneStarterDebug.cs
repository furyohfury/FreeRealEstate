using System;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class LaneStarterDebug : MonoBehaviour
    {
        [Button]
        public void StartSpawn()
        {
            LaneItemSpawner[] findObjectsByType = FindObjectsByType<LaneItemSpawner>(FindObjectsSortMode.None);

            foreach (LaneItemSpawner laneItemSpawner in findObjectsByType)
            {
                laneItemSpawner.StartSpawning();
            }
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
