using TriInspector;
using UnityEngine;

namespace Game
{
    public class GameLoop : Singleton<GameLoop>
    {
        public bool IsActive { get; set; } = false;
        public float CurrentTime { get; private set; } = 0;
        [SerializeField] [RequiredGet(InChildren = true)]
        private ItemLaneMover _itemLaneMover;
        [SerializeField] [RequiredGet(InChildren = true)]
        private LanesSpeedUpdater _lanesSpeedUpdater;

        public void Launch()
        {
            CurrentTime = 0;
            IsActive = true;
        }

        public void Pause()
        {
            IsActive = false;
        }

        public void Resume()
        {
            IsActive = true;
        }
        
        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            CurrentTime += Time.deltaTime;
            _itemLaneMover.MoveItems(Time.deltaTime);
            _lanesSpeedUpdater.UpdateLanesSpeed(CurrentTime);
        }
    }
}
