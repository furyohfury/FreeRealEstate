using TriInspector;
using UnityEngine;

namespace Game
{
    public class GameLoop : Singleton<GameLoop>
    {
        public bool IsActive { get; set; } = false;
        [SerializeField][RequiredGet(InChildren =  true)]
        private ItemLaneMover _itemLaneMover;
        [SerializeField][RequiredGet(InChildren =  true)]
        private LanesSpeedUpdater _lanesSpeedUpdater;
        private float _currentTime = 0;

        public void Restart()
        {
            _currentTime = 0;
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }
            _currentTime += Time.deltaTime;
            _itemLaneMover.MoveItems(Time.deltaTime);
            _lanesSpeedUpdater.UpdateLanesSpeed(_currentTime);
        }
    }
}
