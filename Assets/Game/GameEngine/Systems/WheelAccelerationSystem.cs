using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelAccelerationSystem : IFixedSystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<Wheel> _wheelStash;
		private Stash<AccelerationReq> _accEventStash;
		private Stash<WheelRaycast> _raycastStash;
		private Stash<TransformComp> _transformStash;
		private Stash<RigidBodyComp> _rbStash;
		private Stash<TopSpeed> _speedStash;
		private Stash<PowerCurve> _powerCurveStash;
		private Stash<ForceReq> _forceStash;

		public void OnAwake()
		{
			_wheelStash = World.GetStash<Wheel>();
			_accEventStash = World.GetStash<AccelerationReq>();
			_raycastStash = World.GetStash<WheelRaycast>();
			_transformStash = World.GetStash<TransformComp>();
			_speedStash = World.GetStash<TopSpeed>();
			_rbStash = World.GetStash<RigidBodyComp>();
			_forceStash = World.GetStash<ForceReq>();
			_powerCurveStash = World.GetStash<PowerCurve>();
			_filter = World.Filter
			               .With<AccelerationReq>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				var accelerationEvent = _accEventStash.Get(entity);
				var wheelEntity = accelerationEvent.Entity;

				if (_wheelStash.Has(wheelEntity) == false)
				{
					continue;
				}

				ref var raycastComponent = ref _raycastStash.Get(wheelEntity);

				if (raycastComponent.IsGrounded == false)
				{
					continue;
				}

				var transformComp = _transformStash.Get(wheelEntity);
				var wheelTransform = transformComp.Transform;
				var accelDir = (float3)wheelTransform.forward;
				var wheelComp = _wheelStash.Get(wheelEntity);
				var vehicle = wheelComp.Vehicle.Entity;
				var vehicleRb = _rbStash.Get(vehicle).Rigidbody;
				var vehicleTransform = _transformStash.Get(vehicle).Transform;

				var carSpeed = Vector3.Dot(vehicleTransform.forward, vehicleRb.linearVelocity);
				var topSpeed = _speedStash.Get(vehicle).MaxSpeed;
				var normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / topSpeed);
				var powerCurve = _powerCurveStash.Get(vehicle).Curve;
				var availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelerationEvent.Acceleration.y;
				var wheelForce = accelDir * availableTorque;

				Entity request = World.CreateEntity();
				ref var forceReq = ref _forceStash.Add(request);
				forceReq.Target = wheelEntity;
				forceReq.Force = wheelForce;
				forceReq.Point = wheelTransform.position;
				forceReq.ForceMode = ForceMode.Force;
				Debug.Log("Created forcereq, FOrce =  " + forceReq.Force);
			}
		}

		public void Dispose()
		{
		}
	}
}
