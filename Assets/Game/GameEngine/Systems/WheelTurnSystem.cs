using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelTurnSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<VehicleWheels> _wheelsStash;
		private Stash<Steering> _steeringStash;
		private Stash<FrontWheelTag> _frontWheelsStash;
		private Stash<TransformComp> _transformStash;

		public void OnAwake()
		{
			_wheelsStash = World.GetStash<VehicleWheels>();
			_steeringStash = World.GetStash<Steering>();
			_frontWheelsStash = World.GetStash<FrontWheelTag>();
			_transformStash = World.GetStash<TransformComp>();
			_filter = World.Filter
			               .With<Steering>()
			               .With<VehicleWheels>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				float steering = _steeringStash.Get(entity).Direction;
				List<EntityProvider> wheels = _wheelsStash.Get(entity).Wheels;

				foreach (var wheel in wheels)
				{
					if (_frontWheelsStash.Has(wheel.Entity))
					{
						Transform wheelTransform = _transformStash.Get(wheel.Entity).Transform;
						var steeringLerp = math.unlerp(-1, 1, steering);
						wheelTransform.rotation = quaternion.Euler(new float3(0, math.lerp(-45, 45, steeringLerp), 0));
					}
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
