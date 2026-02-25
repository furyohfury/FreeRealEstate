using UnityEngine;

namespace Game
{
    public sealed class ItemLaneMover : MonoBehaviour
    {
        [SerializeField]
        private LaneSystem _laneSystem;

        public void MoveItems(float deltaTime)
        {
            foreach (var lane in _laneSystem.Lanes)
            {
                if (lane.IsMoving == false)
                {
                    continue;
                }

                foreach (var item in lane.LinkedItems)
                {
                    item.Move(Vector3.back * (lane.Speed * deltaTime));
                }
            }
        }
    }
}
