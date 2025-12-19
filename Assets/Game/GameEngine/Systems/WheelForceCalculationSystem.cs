using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelForceCalculationSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<SuspensionComp> _suspensionStash;
		private Stash<WheelRaycastComp> _raycastStash;
		private Stash<ForceComp> _forceStash;
		private Stash<TransformComp> _transformStash;
		private Stash<RigidBodyComp> _rigidBodyStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<SuspensionComp>()
			               .With<WheelRaycastComp>()
			               .With<TransformComp>()
			               .Build();

			_suspensionStash = World.GetStash<SuspensionComp>();
			_raycastStash = World.GetStash<WheelRaycastComp>();
			_forceStash = World.GetStash<ForceComp>();
			_transformStash = World.GetStash<TransformComp>();
			_rigidBodyStash = World.GetStash<RigidBodyComp>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				SuspensionComp suspensionComp = _suspensionStash.Get(entity);
				float length = suspensionComp.Radius;
				float maxSuspension = suspensionComp.MaxSuspension;
				WheelRaycastComp raycastComp = _raycastStash.Get(entity);
				float distanceToGround = raycastComp.Distance;
				bool isGrounded = raycastComp.IsGrounded;

				if (!isGrounded)
				{
					_forceStash.Remove(entity);
				}
				else
				{
					TransformComp transformComp = _transformStash.Get(entity);
					RigidBodyComp rigidBodyComp = _rigidBodyStash.Get(entity);

					float stiffness = suspensionComp.Stiffness;
					float damper = suspensionComp.Damper;
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

					if (_forceStash.Has(entity))
					{
						ref ForceComp forceComp = ref _forceStash.Get(entity);
						forceComp.Force += finalForce + (float3)t;
						forceComp.Point += (float3)collisionPoint;
					}
					else
					{
						ref ForceComp forceComp = ref _forceStash.Add(entity);
						forceComp.Force += finalForce + (float3)t;
						forceComp.Point += (float3)collisionPoint;
					}
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
