using UnityEngine;

namespace Gameplay
{
	[CreateAssetMenu(
		fileName = nameof(MatchSettings),
		menuName = nameof(Gameplay) + "/" + nameof(MatchSettings))]
	public sealed class MatchSettings : ScriptableObject
	{
		public int PointsToWin = 3;
	}
}