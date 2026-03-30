using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class SessionLauncher : Singleton<SessionLauncher>
    {
        [SerializeField]
        [RequiredGet(InChildren = true)]
        private LanesSessionSpawner _lanesSessionSpawner;
        [SerializeField]
        private bool _launchWithCountdown;
        [SerializeField]
        [RequiredGet(InChildren = true)]
        public LaunchCountDownHandler launchCountdownHandler;

        public async void LaunchSession()
        {
            if (_launchWithCountdown)
            {
                await launchCountdownHandler.CountdownAsync();
            }
            
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
