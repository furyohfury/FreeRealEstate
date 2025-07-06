using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class AnchorPointsComponent
	{
		[SerializeField]
		private Transform[] _anchorPoints;
		private int _index;

		public Transform GetAnchorPoint()
		{
			return _anchorPoints[_index++ % _anchorPoints.Length];
		}
	}
}