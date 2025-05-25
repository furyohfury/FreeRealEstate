using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class PikminTargetComponent
	{
		public GameObject Target
		{
			get => _target;
			set => _target = value;
		}
		
		[SerializeField]
		private GameObject _target;
	}
}