using Scellecs.Morpeh;
using Unity.Mathematics;

namespace GameEngine
{
	public sealed class SteeringDirectionSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<Input> _inputStash;
		private Stash<Steering> _steeringDirectionStash;

		public void OnAwake()
		{
			_inputStash = World.GetStash<Input>();
			_steeringDirectionStash = World.GetStash<Steering>();
			_filter = World.Filter
			               .With<Input>()
			               .With<PlayerTag>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				float2 direction = _inputStash.Get(entity).Direction;

				ref Steering steering = ref _steeringDirectionStash.Has(entity) 
					? ref _steeringDirectionStash.Get(entity) 
					: ref _steeringDirectionStash.Add(entity);
				
				steering.Direction = direction.x;
			}
		}

		public void Dispose()
		{
		}
	}
}
