namespace Game
{
    public class HealthController : Singleton<HealthController>
    {
        public void PenalizeForCollision()
        {
            Health.Instance.CurrentHealth -= GameParams.Instance.Params.PenaltyForCollision;
        }
    }
}
