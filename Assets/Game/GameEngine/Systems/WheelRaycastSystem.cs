using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelRaycastSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<WheelRaycast> _raycastStash;
		private Stash<TransformComp> _transformStash;
		private Stash<Suspension> _suspensionStash;

		public void OnAwake()
		{
			_filter = World.Filter
			               .With<WheelRaycast>()
			               .With<TransformComp>()
			               .With<Suspension>()
			               .Build();

			_raycastStash = World.GetStash<WheelRaycast>();
			_transformStash = World.GetStash<TransformComp>();
			_suspensionStash = World.GetStash<Suspension>();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref WheelRaycast wheelRaycast = ref _raycastStash.Get(entity);
				Suspension suspension = _suspensionStash.Get(entity);

				TransformComp transformComp = _transformStash.Get(entity);
				Transform transform = transformComp.Transform;
				Vector3 position = transform.position;
				float length = suspension.Radius + suspension.MaxSuspension;
				bool raycast = Physics.Raycast(position, -transform.up, out var hit, length);
				wheelRaycast.IsGrounded = raycast;
				wheelRaycast.Normal = hit.normal;

				if (raycast)
				{
					wheelRaycast.Distance = hit.distance;
				}
				else
				{
					wheelRaycast.Distance = 0.0f;
				}
			}
		}

		public void Dispose()
		{
		}
	}
}
