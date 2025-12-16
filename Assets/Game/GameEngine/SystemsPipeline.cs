using Scellecs.Morpeh;
using UnityEngine;

namespace Game.GameEngine
{
	public sealed class SystemsPipeline : MonoBehaviour
	{
		private World _world;

		private void Start()
		{
			_world = World.Default;

			SystemsGroup systemsGroup = _world.CreateSystemsGroup();
			systemsGroup.AddSystem(new DebugSystem());
			_world.AddSystemsGroup(0, systemsGroup);
		}
	}

}
