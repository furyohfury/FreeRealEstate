using System.Collections.Generic;
using Unity.Services.Multiplayer;

namespace Game
{
    public sealed class SessionInfo
    {
        public readonly ISession Session;
        public readonly List<LobbyPlayerInfo> Players = new List<LobbyPlayerInfo>();

        public SessionInfo(ISession session, params LobbyPlayerInfo[] players)
        {
            Session = session;
            Players.AddRange(players);
        }
    }
}
