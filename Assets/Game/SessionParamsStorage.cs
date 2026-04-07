namespace Game
{
    public sealed class SessionParamsStorage
    {
        public SessionParams[] SessionParams;

        public SessionParamsStorage()
        {
        }

        public SessionParamsStorage(SessionParams[] sessionParams)
        {
            SessionParams = sessionParams;
        }
    }
}
