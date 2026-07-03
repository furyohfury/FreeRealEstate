using System.Collections.Generic;

namespace Game.Scripts.Shooter
{
    public sealed class PlayersProvider
    {
        public IReadOnlyCollection<Player> OtherPlayers => _otherPlayers;
        public Player MyPlayer { get; set; }
        private readonly HashSet<Player> _otherPlayers = new HashSet<Player>();

        public void AddOtherPlayer(Player player)
        {
            _otherPlayers.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            _otherPlayers.Remove(player);
        }

        public void Register(Player player)
        {
            if (player.NetworkObject.IsOwner)
            {
                MyPlayer = player;
            }
            else
            {
                _otherPlayers.Add(player);
            }
        }
    }
}
