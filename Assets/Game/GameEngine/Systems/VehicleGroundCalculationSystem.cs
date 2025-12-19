using Scellecs.Morpeh;
using Unity.Mathematics;

namespace GameEngine
{
	public sealed class VehicleGroundCalculationSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<WheelsComp> _wheelsStash;
		private Stash<VehicleGroundComp> _groundStash;
		private Stash<WheelRaycastComp> _raycastStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<WheelComp>()
			               .Build();

			_wheelsStash = World.GetStash<WheelsComp>();
			_raycastStash = World.GetStash<WheelRaycastComp>();
			_groundStash = World.GetStash<VehicleGroundComp>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				WheelsComp wheelsComp = _wheelsStash.Get(entity);
				float3 normal = float3.zero;

				foreach (Entity wheel in wheelsComp.Wheels)
				{
					WheelRaycastComp wheelRaycastComp = _raycastStash.Get(wheel);
					normal += wheelRaycastComp.Normal;
				}

				ref VehicleGroundComp vehicleGroundComp = ref _groundStash.Get(entity);
				vehicleGroundComp.Normal = normal;
			}
		}

		public void Dispose()
		{
		}
	}
}
