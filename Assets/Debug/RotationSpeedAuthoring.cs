using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Debugging
{   
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float3 rotationSpeed;
        public class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed
                                     {
                                         Direction = authoring.rotationSpeed
                                     });
            }
        }
    }
}