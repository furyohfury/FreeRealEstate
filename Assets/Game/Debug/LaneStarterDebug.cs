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
            Lane[] lanes = LaneSystem.Instance.Lanes;

            foreach (Lane lane in lanes)
            {
                lane.StartSpawning();
            }
            
            FindAnyObjectByType<GameLoop>().IsActive = true;
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
