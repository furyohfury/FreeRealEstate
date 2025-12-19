using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class WheelAccelerationSystem : ISystem
	{
		public World World { get; set; }
		private Filter _filter;
		private Stash<WheelComp> _wheelStash;
		private Stash<AccelerationEvent> _accEventStash;
		private Stash<WheelRaycastComp> _raycastStash;
		private Stash<TransformComp> _transformStash;

		public void OnAwake()
		{
			_wheelStash = World.GetStash<WheelComp>();
			_accEventStash = World.GetStash<AccelerationEvent>();
			_raycastStash = World.GetStash<WheelRaycastComp>();
			_transformStash = World.GetStash<TransformComp>();
			_filter = World.Filter
			               .With<AccelerationEvent>()
			               .Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				AccelerationEvent accelerationEvent = _accEventStash.Get(entity);
				Entity wheelEntity = accelerationEvent.Entity;

				if (_wheelStash.Has(wheelEntity) == false)
				{
					continue;
				}

				ref var raycastComponent = ref _raycastStash.Get(wheelEntity);

				if (raycastComponent.IsGrounded == false)
				{
					continue;
				}

				TransformComp transformComp = _transformStash.Get(wheelEntity);
				Transform wheelTransform = transformComp.Transform;
				var accelDir = wheelTransform.forward;
				// var carSpeed = Vector3.Dot(car) НУЖНА ЕЩЕ ТАЧКА ПО ВИДОСУ БЛЯ))
			}
		}

		public void Dispose()
		{
		}
	}
}
