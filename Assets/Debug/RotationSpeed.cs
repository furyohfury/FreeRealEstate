using Unity.Entities;
using Unity.Mathematics;

namespace Debugging
{
    public struct RotationSpeed : IComponentData
    {
        public float3 Direction;
    }
}