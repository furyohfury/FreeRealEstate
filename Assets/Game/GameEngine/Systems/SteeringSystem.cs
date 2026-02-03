using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace GameEngine
{
	public sealed class SteeringSystem : IFixedSystem
	{
		public World World { get; set; }
		private Stash<TransformComp> _transformStash;
		private Stash<RigidBodyComp> _rbStash;
		private Filter _filter;
		private Stash<VehicleWheels> _vehicleWheelsStash;
		private Stash<WheelRaycast> _raycastStash;
		private Stash<TireGripComp> _tireGripStash;
		private Stash<ForceReq> _forceReqStash;
		private Stash<Mass> _massStash;

		public void OnAwake()
		{
			_transformStash = World.GetStash<TransformComp>();
			_rbStash = World.GetStash<RigidBodyComp>();
			_vehicleWheelsStash = World.GetStash<VehicleWheels>();
			_raycastStash = World.GetStash<WheelRaycast>();
			_tireGripStash = World.GetStash<TireGripComp>();
			_forceReqStash = World.GetStash<ForceReq>();
			_massStash = World.GetStash<Mass>();
			_filter = World.Filter
			     .With<VehicleWheels>()
			     .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity vehicle in _filter)
			{
				List<EntityProvider> wheels = _vehicleWheelsStash.Get(vehicle).Wheels;

				foreach (EntityProvider provider in wheels)
				{
					Entity wheel = provider.Entity;
					WheelRaycast wheelRaycast = _raycastStash.Get(wheel);
					bool isGrounded = wheelRaycast.IsGrounded;

					if (!isGrounded)
					{
						continue;
					}

					float wheelMass = _massStash.Get(wheel).Value;
					Rigidbody rigidbody = _rbStash.Get(vehicle).Rigidbody;
					Transform wheelTransform = _transformStash.Get(wheel).Transform;
					var tireGripFactor = _tireGripStash.Get(wheel).GripFactor;
					Vector3 steeringDir = wheelTransform.right;
					Vector3 tireWorldVel = rigidbody.GetPointVelocity(wheelTransform.position);
					float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
					float desiredVelChange = -steeringVel * tireGripFactor;
					float desiredAccel = desiredVelChange / deltaTime;
					Entity newEntity = World.CreateEntity();
					ref ForceReq forceReq = ref _forceReqStash.Add(newEntity);
					forceReq.Target = wheel;
					forceReq.Force = steeringDir * (desiredAccel * wheelMass);
					forceReq.Point = wheelTransform.position;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
