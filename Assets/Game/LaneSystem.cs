using UnityEngine;

namespace Game
{
    public sealed class LaneSystem : MonoBehaviour
    {
        [field: SerializeField]
        public Lane[] Lanes { get; private set; }
    }
}
