using UnityEngine;

namespace Game
{
    public sealed class ItemLaneMover : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private LaneSystem _laneSystem;

        private void Update()
        {
            foreach (var lane in _laneSystem.Lanes)
            {
                if (lane.IsMoving == false)
                {
                    continue;
                }

                foreach (var item in lane.LinkedItems)
                {
                    item.Move(Vector3.back * (lane.Speed * Time.deltaTime));
                }
            }
        }
    }
}
