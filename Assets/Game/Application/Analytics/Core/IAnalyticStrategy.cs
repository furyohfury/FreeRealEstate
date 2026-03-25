namespace Game.Application.Ads
{
    public interface IAnalyticStrategy
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, float value);
    }
}
