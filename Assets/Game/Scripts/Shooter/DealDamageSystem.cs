using System;

namespace Game.Scripts.Shooter
{
    public sealed class DealDamageSystem
    {
        public event Action<DealDamageEvent> OnDealDamage;
        public event Action<KillEvent> OnKill;

        public void DealDamage(DealDamageEvent dealDamageEvent, IHealth health)
        {
            health.TakeDamage(dealDamageEvent.Damage);
            OnDealDamage?.Invoke(dealDamageEvent);

            if (health.GetCurrentHealth() <= 0)
            {
                OnKill?.Invoke(new KillEvent(dealDamageEvent.TargetNetworkObjectId, dealDamageEvent.SourceNetworkObjectId));
            }
        }
    }
}
