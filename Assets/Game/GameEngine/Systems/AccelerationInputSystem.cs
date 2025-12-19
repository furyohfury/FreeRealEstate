using Scellecs.Morpeh;
using Unity.Mathematics;

namespace GameEngine
{
	public sealed class AccelerationInputSystem : ISystem
	{
		public World World { get; set; }
		private Filter _inputFilter;
		private Filter _playerFilter;
		private Stash<InputComp> _inputStash;
		private Stash<AccelerationEvent> _accelerationStash;
		private Stash<PlayerTag> _playerStash;

		public void OnAwake()
		{
			_inputStash = World.GetStash<InputComp>();
			_accelerationStash = World.GetStash<AccelerationEvent>();
			_playerStash = World.GetStash<PlayerTag>();
			_inputFilter = World.Filter
			                    .With<InputComp>()
			                    .Build();

			_playerFilter = World.Filter
			                     .With<PlayerTag>()
			                     .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _inputFilter)
			{
				InputComp inputComp = _inputStash.Get(entity);
				float2 direction = inputComp.Direction;

				foreach (Entity player in _playerFilter)
				{
					Entity ev = World.CreateEntity();
					ref var accEvent = ref _accelerationStash.Add(ev);
					accEvent.Acceleration = new float3(direction.x, direction.y, 0);
					accEvent.Entity = player;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
