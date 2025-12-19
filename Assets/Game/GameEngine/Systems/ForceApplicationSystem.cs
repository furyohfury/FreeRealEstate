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
		private Stash<ForceComp> _forceStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<ForceComp>()
			               .With<RigidBodyComp>()
			               .Build();

			_rigidbodyStash = World.GetStash<RigidBodyComp>();
			_forceStash = World.GetStash<ForceComp>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				Debug.Log("force app. Entity: " + entity.Id);
				ForceComp forceComp = _forceStash.Get(entity);
				float3 force = forceComp.Force;
				RigidBodyComp rigidBodyComp = _rigidbodyStash.Get(entity);
				Rigidbody rigidbody = rigidBodyComp.Rigidbody;
				Vector3 point = forceComp.Point;

				rigidbody.AddForceAtPosition(force, point, forceComp.ForceMode);
			}
		}

		public void Dispose()
		{
		}
	}
}
