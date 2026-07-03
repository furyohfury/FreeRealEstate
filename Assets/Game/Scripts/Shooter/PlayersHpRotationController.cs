using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class PlayersHpRotationController : ILateTickable
    {
        private readonly PlayersProvider _playersProvider;

        public PlayersHpRotationController(PlayersProvider playersProvider)
        {
            _playersProvider = playersProvider;
        }

        public void LateTick()
        {
            if (_playersProvider.MyPlayer == null)
            {
                return;
            }
            
            var players = _playersProvider.OtherPlayers;

            foreach (var player in players)
            {
                player.TurnHpUiTo(_playersProvider.MyPlayer.Position);
            }
        }
    }
}
