using Unity.Entities;
using Unity.Mathematics;

namespace Debugging
{
    public struct CannonBall : IComponentData
    {
        public float3 Velocity;
    }
}
