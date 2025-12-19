using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelRaycastSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<WheelRaycastComp> _raycastStash;
		private Stash<TransformComp> _transformStash;
		private Stash<SuspensionComp> _suspensionStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<WheelRaycastComp>()
			               .With<TransformComp>()
			               .With<SuspensionComp>()
			               .Build();

			_raycastStash = World.GetStash<WheelRaycastComp>();
			_transformStash = World.GetStash<TransformComp>();
			_suspensionStash = World.GetStash<SuspensionComp>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref WheelRaycastComp wheelRaycastComp = ref _raycastStash.Get(entity);
				SuspensionComp suspensionComp = _suspensionStash.Get(entity);

				TransformComp transformComp = _transformStash.Get(entity);
				Transform transform = transformComp.Transform;
				Vector3 position = transform.position;
				float length = suspensionComp.Radius + suspensionComp.MaxSuspension;
				bool raycast = Physics.Raycast(position, -transform.up, out var hit, length);
				wheelRaycastComp.IsGrounded = raycast;
				wheelRaycastComp.Normal = hit.normal;

				if (raycast)
				{
					wheelRaycastComp.Distance = hit.distance;
				}
				else
				{
					wheelRaycastComp.Distance = 0.0f;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
