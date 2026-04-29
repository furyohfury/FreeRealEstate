using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Debugging
{
    public partial struct PlayerSystem : ISystem
    {
        // Because OnUpdate accesses a managed object (the camera), we cannot Burst compile 
        // this method, so we don't use the [BurstCompile] attribute here.
        public void OnUpdate(ref SystemState state)
        {
            Debug.Log("PlayerSystem::OnUpdate");
            
            float horizontal = 0;

            if (Keyboard.current.aKey.isPressed)
            {
                horizontal = -1;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                horizontal = 1;
            }

            float vertical = 0;

            if (Keyboard.current.wKey.isPressed)
            {
                vertical = 1;
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                vertical = -1;
            }

            var movement = new float3(horizontal, 0, vertical);
            movement *= SystemAPI.Time.DeltaTime;

            foreach (var playerTransform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Player>())
            {
                // move the player tank
                playerTransform.ValueRW.Position += movement;

                // move the camera to follow the player
                var cameraTransform = Camera.main!.transform;
                cameraTransform.position = playerTransform.ValueRO.Position;
                cameraTransform.position -= 10.0f * (Vector3)playerTransform.ValueRO.Forward(); // move the camera back from the player
                cameraTransform.position += new Vector3(0, 5f, 0); // raise the camera by an offset
                cameraTransform.LookAt(playerTransform.ValueRO.Position); // look at the player
            }
        }
    }
}
