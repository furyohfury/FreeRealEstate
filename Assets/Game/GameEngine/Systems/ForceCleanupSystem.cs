using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class ForceCleanupSystem : IFixedSystem
	{
		public World World
		{
			get;
			set;
		}
		private Filter _filter;
		private Stash<ForceComp> _forceStash;

		public void OnAwake()
		{
			_forceStash = World.GetStash<ForceComp>();
			_filter = World.Filter
			               .With<ForceComp>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				Debug.Log("force clean. Entity: " + entity.Id);
				_forceStash.Remove(entity);
			}
		}

		public void Dispose()
		{
		}
	}
}
