using UnityEngine;

namespace Gameplay
{
	public sealed class GameFinisher
	{
		public void FinishGame(Player player)
		{
			Debug.Log($"<color=yellow> Player {player.ToString()} won!");
		}
	}
}