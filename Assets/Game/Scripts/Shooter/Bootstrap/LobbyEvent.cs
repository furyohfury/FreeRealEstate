using Unity.Services.Multiplayer;

namespace Game
{
    public struct LobbyEvent
    {
        public readonly LobbyEventType EventType;
        public readonly ISession Session;

        public LobbyEvent(LobbyEventType eventType, ISession session)
        {
            EventType = eventType;
            Session = session;
        }
    }
}