namespace Game.Scripts.Shooter
{
    public class DealDamageEvent
    {
        public ulong TargetNetworkObjectId;
        public float Damage;
        public ulong SourceNetworkObjectId;

        public DealDamageEvent(ulong targetNetworkObjectId, float damage, ulong sourceNetworkObjectId)
        {
            TargetNetworkObjectId = targetNetworkObjectId;
            Damage = damage;
            SourceNetworkObjectId = sourceNetworkObjectId;
        }
    }
}