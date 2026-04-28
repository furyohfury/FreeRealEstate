using Unity.Entities;

namespace Debugging
{
    public struct Tank : IComponentData
    {
        public Entity Cannon;
        public Entity Turret;
    }
}
