using UnityEngine;

namespace Game
{
    public sealed class LaneSystem : Singleton<LaneSystem>
    {
        [field: SerializeField]
        public Lane[] Lanes { get; set; }
    }
}
