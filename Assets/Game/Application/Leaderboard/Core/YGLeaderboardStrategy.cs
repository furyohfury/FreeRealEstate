using System;
using System.Threading;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace Game.Application.Leaderboard
{
    public class YGLeaderboardStrategy : LeaderboardStrategy
    {
        private bool _authed;

        public async override Awaitable SendResult(float result)
        {
            bool authed = YG2.player.auth;
            _authed = authed;

            if (!_authed)
            {
                Debug.Log($"<color=red>YGLeaderboardStrategy: Wasnt authed. Not sending leaderboard result</color>");
                return;
            }

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            string sessionParamsId = GameParamsService.Instance.SessionParams.Id;
            string lbId = SessionParamsToLeaderboardIdConverter.Convert(sessionParamsId);

            try
            {
                LBData leaderboardData = await YGLeaderboardProvider.Instance.GetLeaderboard(lbId, cts.Token);
                LBCurrentPlayerData currentPlayerData = leaderboardData.currentPlayer;

                if (currentPlayerData == null
                    || result > currentPlayerData.score)
                {
                    YG2.SetLBTimeConvert(lbId, result);
                    Debug.Log($"<color=green>YGLeaderboardStrategy: Set leaderboard {lbId} result {result}</color>");
                }
                else
                {
                    Debug.Log($"<color=green>YGLeaderboardStrategy currentPlayerData.score is {currentPlayerData.score}, result is {result}</color>");
                }
            }
            catch (OperationCanceledException e)
            {
                Debug.LogError(e.Message);
            }
        }

        public override LeaderboardEntry[] GetEntries(string leaderboardId)
        {
            return null;
        }
    }
}
