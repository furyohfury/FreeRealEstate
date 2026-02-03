using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class ForceDebugSystem : IFixedSystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<ForceReq> _forceStash;
		private Stash<GizmosForceDrawerComp> _gizmosDrawerStash;

		public void OnAwake()
		{
			_forceStash = World.GetStash<ForceReq>();
			_gizmosDrawerStash = World.GetStash<GizmosForceDrawerComp>();
			_filter = World.Filter
			               .With<ForceReq>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				var forceReq = _forceStash.Get(entity);
				float3 force = forceReq.Force;
				Entity target = forceReq.Target;
				var drawer = _gizmosDrawerStash.Get(target).GizmosForceDrawer;
				drawer.Direction += (Vector3)force;
			}
		}

		public void Dispose()
		{
		}
	}
}
