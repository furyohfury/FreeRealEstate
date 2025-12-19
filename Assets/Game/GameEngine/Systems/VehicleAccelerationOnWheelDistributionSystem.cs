using System.Collections.Generic;
using Scellecs.Morpeh;

namespace GameEngine
{
	public sealed class VehicleAccelerationWheelDistributionSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<AccelerationEvent> _accStash;
		private Stash<WheelsComp> _wheelsStash;

		public void OnAwake()
		{
			_wheelsStash = World.GetStash<WheelsComp>();
			_accStash = World.GetStash<AccelerationEvent>();
			_filter = World.Filter
			               .With<AccelerationEvent>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				AccelerationEvent accelerationEvent = _accStash.Get(entity);
				Entity player = accelerationEvent.Entity;

				if (_wheelsStash.Has(player))
				{
					ref var wheelsComp = ref _wheelsStash.Get(player);
					List<Entity> wheels = wheelsComp.Wheels;
					foreach (var wheel in wheels)
					{
						Entity eventEntity = World.CreateEntity();
						ref var wheelAccEvent = ref _accStash.Add(eventEntity);
						wheelAccEvent.Acceleration = accelerationEvent.Acceleration;
						wheelAccEvent.Entity = wheel;
					}
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
