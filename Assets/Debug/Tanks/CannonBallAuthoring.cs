using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Debugging
{
    public class CannonBallAuthoring : MonoBehaviour
    {
        public float3 velocity;
        public class CannonBallBaker : Baker<CannonBallAuthoring>
        {
            public override void Bake(CannonBallAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CannonBall
                                     {
                                         Velocity = authoring.velocity
                                     });
            }
        }
    }
}
