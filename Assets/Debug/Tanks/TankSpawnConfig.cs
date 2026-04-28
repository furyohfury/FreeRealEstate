using Unity.Entities;

namespace Debugging
{
    public struct TankSpawnConfig : IComponentData
    {
        public Entity TankPrefab;
        public Entity CannonBallPrefab;
        public int TankCount;
    }
}