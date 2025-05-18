using System;
using UnityEngine;
using UnityEngine.AI;

namespace GameEngine
{
	[Serializable]
	public sealed class NavMeshComponent : IComponent
	{
		public Vector3 Velocity => _navMeshAgent.velocity;
		
		[SerializeField]
		private NavMeshAgent _navMeshAgent;
	}
}