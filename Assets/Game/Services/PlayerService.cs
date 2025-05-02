namespace Game
{
	public sealed class PlayerService
	{
		public Player Player => _player;

		private readonly Player _player;

		public PlayerService(Player player)
		{
			_player = player;
		}
	}
}