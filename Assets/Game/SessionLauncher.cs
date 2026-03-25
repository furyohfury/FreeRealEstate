using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class SessionLauncher : Singleton<SessionLauncher>
    {
        [SerializeField]
        [RequiredGet(InChildren = true)]
        private LanesSessionSpawner _lanesSessionSpawner;

        public void LaunchSession()
        {
            InitLanes();
            GameLoop.Instance.Launch();
        }

        private void InitLanes()
        {
            _lanesSessionSpawner.SpawnLanes();
            Lane[] lanes = LaneSystem.Instance.Lanes;
            foreach (Lane lane in lanes)
            {
                lane.StartSpawning();
            }
        }
    }
}
