namespace Game
{
    public sealed class LobbyPlayerInfo
    {
        public readonly string Id;
        public readonly string Nickname;
        public ulong ClientID;

        public LobbyPlayerInfo(string id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
    }
}