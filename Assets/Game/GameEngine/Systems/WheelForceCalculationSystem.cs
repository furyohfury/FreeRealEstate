using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelForceCalculationSystem : IFixedSystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<Suspension> _suspensionStash;
		private Stash<WheelRaycast> _raycastStash;
		private Stash<ForceReq> _forceStash;
		private Stash<TransformComp> _transformStash;
		private Stash<RigidBodyComp> _rigidBodyStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<Suspension>()
			               .With<WheelRaycast>()
			               .With<TransformComp>()
			               .Build();

			_suspensionStash = World.GetStash<Suspension>();
			_raycastStash = World.GetStash<WheelRaycast>();
			_forceStash = World.GetStash<ForceReq>();
			_transformStash = World.GetStash<TransformComp>();
			_rigidBodyStash = World.GetStash<RigidBodyComp>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				Suspension suspension = _suspensionStash.Get(entity);
				float length = suspension.Radius;
				float maxSuspension = suspension.MaxSuspension;
				WheelRaycast raycast = _raycastStash.Get(entity);
				float distanceToGround = raycast.Distance;
				bool isGrounded = raycast.IsGrounded;

				if (!isGrounded)
				{
					_forceStash.Remove(entity);
				}
				else
				{
					TransformComp transformComp = _transformStash.Get(entity);
					RigidBodyComp rigidBodyComp = _rigidBodyStash.Get(entity);

					float stiffness = suspension.Stiffness;
					float damper = suspension.Damper;
					Transform transform = transformComp.Transform;
					Rigidbody rigidbody = rigidBodyComp.Rigidbody;
					float compression = distanceToGround / (maxSuspension + length);
					compression = -compression + 1; // просто для инверсии. Типа Clamp01
					Vector3 force = transform.up * (compression * stiffness);

					Vector3 collisionPoint = transform.position - transform.up * distanceToGround;
					Vector3 velocityAtTouch = rigidbody.GetPointVelocity(collisionPoint);
					Vector3 t = transform.InverseTransformDirection(velocityAtTouch);

					// local x and z directions = 0
					t.z = t.x = 0;

					// back to world space * -damping
					Vector3 damping = transform.TransformDirection(t) * -damper;
					float3 finalForce = force + damping;

					// VERY simple turning - force rigidbody in direction of wheel
					t = rigidbody.transform.InverseTransformDirection(velocityAtTouch);
					t.y = 0;
					t.z = 0;
					t = transform.TransformDirection(t);

					Entity request = World.CreateEntity();
					ref ForceReq forceReq = ref _forceStash.Add(request);
					forceReq.Target = entity;
					forceReq.Force = finalForce + (float3)t;
					forceReq.Point = collisionPoint;
					forceReq.ForceMode = ForceMode.Force;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
