namespace Game
{
    public struct PlayerViewData
    {
        public int PlayerId;
        public int Order;

        public PlayerViewData(int playerId, int order)
        {
            PlayerId = playerId;
            Order = order;
        }
    }
}
