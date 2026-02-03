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
			systemsGroup.AddSystem(new AccelerationInputSystem());

			systemsGroup.AddSystem(new WheelRaycastSystem());
			systemsGroup.AddSystem(new WheelForceCalculationSystem());
			systemsGroup.AddSystem(new SteeringDirectionSystem());
			systemsGroup.AddSystem(new WheelTurnSystem());
			systemsGroup.AddSystem(new VehicleAccelerationWheelDistributionSystem());
			systemsGroup.AddSystem(new WheelAccelerationSystem());
			systemsGroup.AddSystem(new SteeringSystem());
			systemsGroup.AddSystem(new ForceDebugSystem());
			systemsGroup.AddSystem(new ForceApplicationSystem());

			// Cleanup
			systemsGroup.AddSystem(new ForceCleanupSystem());
			systemsGroup.AddSystem(new AccelerationCleanupSystem());
			systemsGroup.AddSystem(new ForceDebugCleanupSystem());
			// systemsGroup.AddSystem(new InputCleanupSystem());

			_world.AddSystemsGroup(0, systemsGroup);
		}
	}
}
