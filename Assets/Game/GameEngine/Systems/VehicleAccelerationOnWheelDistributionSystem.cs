using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class VehicleAccelerationWheelDistributionSystem : IFixedSystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<AccelerationReq> _accStash;
		private Stash<VehicleWheels> _wheelsStash;

		public void OnAwake()
		{
			_wheelsStash = World.GetStash<VehicleWheels>();
			_accStash = World.GetStash<AccelerationReq>();
			_filter = World.Filter
			               .With<AccelerationReq>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				AccelerationReq accelerationReq = _accStash.Get(entity);
				Entity player = accelerationReq.Entity;

				if (!_wheelsStash.Has(player))
				{
					continue;
				}

				ref var wheelsComp = ref _wheelsStash.Get(player);
				int wheelsCount = wheelsComp.Wheels.Count;
				Entity[] wheels = new Entity[wheelsCount];

				for (int i = 0; i < wheelsCount; i++)
				{
					wheels[i] = wheelsComp.Wheels[i].Entity;
				}

				Debug.Log("Disctibuted acc on entities:  " + wheelsCount);

				foreach (var wheel in wheels)
				{
					Entity eventEntity = World.CreateEntity();
					ref var wheelAccEvent = ref _accStash.Add(eventEntity);
					wheelAccEvent.Acceleration = accelerationReq.Acceleration;
					wheelAccEvent.Entity = wheel;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
