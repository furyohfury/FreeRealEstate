namespace Game.Scripts.Shooter
{
    public class KillEvent
    {
        public ulong TargetNetworkObjectId;
        public ulong KillerNetworkObjectId;

        public KillEvent(ulong targetNetworkObjectId, ulong killerNetworkObjectId)
        {
            TargetNetworkObjectId = targetNetworkObjectId;
            KillerNetworkObjectId = killerNetworkObjectId;
        }
    }
}
