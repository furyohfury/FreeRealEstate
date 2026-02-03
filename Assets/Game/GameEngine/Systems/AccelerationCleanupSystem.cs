using Scellecs.Morpeh;

namespace GameEngine
{
	public sealed class AccelerationCleanupSystem : IFixedSystem
	{
		public World World
		{
			get;
			set;
		}
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<AccelerationReq>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				World.RemoveEntity(entity);
			}
		}

		public void Dispose()
		{
		}
	}
}
