namespace Game.Scripts.Shooter
{
    public interface IHealth   
    {
        ulong OwnerClientId { get; }
        void TakeDamage(float damage);
        float GetCurrentHealth();
    }
}
