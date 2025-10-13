namespace Gameplay
{
	public sealed class GameFinisher
	{
		public void FinishGame(Player player)
		{
			UnityEngine.Debug.Log($"<color=yellow> Player {player.ToString()} won!");
		}
	}
}