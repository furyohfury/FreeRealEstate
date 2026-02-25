using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class SessionLauncher : Singleton<SessionLauncher>
    {
        [SerializeField]
        [RequiredGet(InChildren = true)]
        private LanesSessionSpawner _lanesSessionSpawner;

        private void Start()
        {
            _lanesSessionSpawner.SpawnLanes();
        }
    }
}
