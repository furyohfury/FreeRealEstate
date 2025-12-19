using Scellecs.Morpeh;
using UnityEngine;

namespace GameEngine
{
	public sealed class SystemsPipeline : MonoBehaviour
	{
		private World _world;

		private void Start()
		{
			_world = World.Default;
			SystemsGroup systemsGroup = _world.CreateSystemsGroup();

			// systemsGroup.AddSystem(new DebugSystem());
			systemsGroup.AddSystem(new InputSystem());
			systemsGroup.AddSystem(new WheelRaycastSystem());
			systemsGroup.AddSystem(new WheelForceCalculationSystem());
			systemsGroup.AddSystem(new ForceApplicationSystem());
			
			// Cleanup
			systemsGroup.AddSystem(new ForceCleanupSystem());
			
			_world.AddSystemsGroup(0, systemsGroup);
		}
	}
}
