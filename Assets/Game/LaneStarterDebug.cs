using TriInspector;
using UnityEngine;

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
    }
}
