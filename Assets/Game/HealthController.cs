namespace Game
{
    public class HealthController : Singleton<HealthController>
    {
        public void RewardForRightColor()
        {
            Health.Instance.CurrentHealth += GameParamsService.Instance.SessionParams.RewardForRightItemColor;
        }
        
        public void PenalizeForWrongColor()
        {
            Health.Instance.CurrentHealth -= GameParamsService.Instance.SessionParams.PenaltyForWrongItemColor;
        }
        
        public void PenalizeForCollision()
        {
            Health.Instance.CurrentHealth -= GameParamsService.Instance.SessionParams.PenaltyForCollision;
        }
    }
}
