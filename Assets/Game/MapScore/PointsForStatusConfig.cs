using System.Collections.Generic;
using Game.ElementHandle;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scoring
{
	[CreateAssetMenu(fileName = "PointsForStatusConfig", menuName = "Points/PointsForStatusConfig")]
	public sealed class PointsForStatusConfig : SerializedScriptableObject
	{
		public IReadOnlyDictionary<ClickStatus, int> Points => _points;

		[SerializeField]
		private Dictionary<ClickStatus, int> _points;
	}
}