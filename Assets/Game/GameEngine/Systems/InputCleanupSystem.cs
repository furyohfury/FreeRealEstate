using Scellecs.Morpeh;
using Unity.Mathematics;

namespace GameEngine
{
	public sealed class InputCleanupSystem : ISystem
	{
		public World World { get; set; }
		private Stash<Input> _inputStash;
		private Filter _filter;

		public void OnAwake()
		{
			_inputStash = World.GetStash<Input>();
			_filter = World.Filter
			               .With<Input>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref Input input = ref _inputStash.Get(entity);
				input.Direction = float2.zero;
			}
		}

		public void Dispose()
		{
		}
	}
}
