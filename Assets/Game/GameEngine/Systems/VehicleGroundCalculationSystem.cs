using Scellecs.Morpeh;
using Unity.Mathematics;

namespace GameEngine
{
	public sealed class VehicleGroundCalculationSystem : ISystem // not needed?
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<VehicleWheels> _wheelsStash;
		private Stash<VehicleGround> _groundStash;
		private Stash<WheelRaycast> _raycastStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<Wheel>()
			               .Build();

			_wheelsStash = World.GetStash<VehicleWheels>();
			_raycastStash = World.GetStash<WheelRaycast>();
			_groundStash = World.GetStash<VehicleGround>();
		}

		public void OnUpdate(float deltaTime)
		{
			// foreach (var entity in _filter)
			// {
			// 	VehicleWheels vehicleWheels = _wheelsStash.Get(entity);
			// 	float3 normal = float3.zero;
			//
			// 	foreach (Entity wheel in vehicleWheels.Wheels)
			// 	{
			// 		WheelRaycast wheelRaycast = _raycastStash.Get(wheel);
			// 		normal += wheelRaycast.Normal;
			// 	}
			//
			// 	ref VehicleGround vehicleGround = ref _groundStash.Get(entity);
			// 	vehicleGround.Normal = normal;
			// }
		}

		public void Dispose()
		{
		}
	}
}
