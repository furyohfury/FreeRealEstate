namespace Game.Application.Ads
{
    public interface IAnalyticMediator
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, float value);
    }
}
