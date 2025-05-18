using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class MoveTransformComponent : IComponent
	{
		[SerializeField]
		private Transform _transform;
		
		
	}
}