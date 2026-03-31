using UnityEngine;

namespace Game
{
    public struct CollisionEventData
    {
        public Item ControlledItem;
        public Item HitItem;
        public Vector3 HitPoint;
    }
}
