using Scellecs.Morpeh;
using UnityEngine;

namespace Game.GameEngine
{
	public sealed class DebugSystem : ISystem
	{
		public World World
		{
			get;
			set;
		}

		public void OnAwake()
		{
			
		}

		public void OnUpdate(float deltaTime)
		{
			Debug.Log("DebugSystem OnUpdate");
		}

		public void Dispose()
		{
			
		}
	}
}
