using YG;

namespace Game.Application.Leaderboard
{
    public class YGLeaderboardStrategy : LeaderboardStrategy
    {
        public override void SendResult(float result)
        {
            string sessionParamsId = GameParamsService.Instance.SessionParams.Id;
            string lbId = SessionParamsToLeaderboardIdConverter.Convert(sessionParamsId);
            YG2.SetLBTimeConvert(lbId, result);
        }

        public override LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return null;
        }
    }
}
