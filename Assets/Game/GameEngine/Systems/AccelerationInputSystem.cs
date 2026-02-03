using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class AccelerationInputSystem : IFixedSystem
	{
		public World World { get; set; }
		private Filter _inputFilter;
		private Filter _playerFilter;
		private Stash<Input> _inputStash;
		private Stash<AccelerationReq> _accelerationStash;
		private Stash<PlayerTag> _playerStash;
		private Stash<PowerComp> _powerStash;

		public void OnAwake()
		{
			_inputStash = World.GetStash<Input>();
			_accelerationStash = World.GetStash<AccelerationReq>();
			_playerStash = World.GetStash<PlayerTag>();
			_powerStash = World.GetStash<PowerComp>();
			_inputFilter = World.Filter
			                    .With<Input>()
			                    .Build();

			_playerFilter = World.Filter
			                     .With<PlayerTag>()
			                     .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _inputFilter)
			{
				Input input = _inputStash.Get(entity);
				float2 direction = input.Direction;

				if (direction.y == 0)
				{
					continue;
				}

				foreach (Entity player in _playerFilter)
				{
					Entity ev = World.CreateEntity();
					float power = _powerStash.Get(player).Power;
					ref var accEvent = ref _accelerationStash.Add(ev);
					accEvent.Acceleration = new float3(0, direction.y * power, 0);
					accEvent.Entity = player;
					Debug.Log("created accEvent from input, acc = " + accEvent.Acceleration);
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
