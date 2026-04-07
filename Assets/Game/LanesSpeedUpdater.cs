using UnityEngine;

namespace Game
{
    public sealed class LanesSpeedUpdater : MonoBehaviour
    {
        public void UpdateLanesSpeed(float sessionTime, float deltaTime)
        {
            Lane[] lanes = LaneSystem.Instance.Lanes;
            float lanesSpeed = GameParamsService.Instance.SessionParams.LanesSpeedFormula.GetLanesSpeed(sessionTime);

            for (int i = 0; i < lanes.Length; i++)
            {
                lanes[i].Speed = lanesSpeed;
                lanes[i].UpdateVisualOffset(deltaTime);
            }
        }
    }
}
