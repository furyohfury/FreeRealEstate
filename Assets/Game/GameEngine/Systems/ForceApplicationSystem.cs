using Scellecs.Morpeh;
using Unity.Mathematics;
using UnityEngine;

namespace GameEngine
{
	public sealed class ForceApplicationSystem : IFixedSystem
	{
		public World World { get; set; }

		private Filter _filter;
		private Stash<RigidBodyComp> _rigidbodyStash;
		private Stash<ForceReq> _forceStash;

		public void OnAwake()
		{
			_rigidbodyStash = World.GetStash<RigidBodyComp>();
			_forceStash = World.GetStash<ForceReq>();
			_filter = World.Filter
			               .With<ForceReq>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ForceReq forceReq = _forceStash.Get(entity);
				Entity target = forceReq.Target;
				RigidBodyComp rigidBodyComp = _rigidbodyStash.Get(target, out bool exist);

				if (exist)
				{
					Rigidbody rigidbody = rigidBodyComp.Rigidbody;
					float3 force = forceReq.Force;
					Vector3 point = forceReq.Point;
					ForceMode forceMode = forceReq.ForceMode;
					rigidbody.AddForceAtPosition(force, point, forceMode);
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
