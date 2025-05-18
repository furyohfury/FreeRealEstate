using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class PointsValueComponent : IComponent
	{
		public int Value => _value;
		
		[SerializeField]
		private int _value;
	}
}