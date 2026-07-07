namespace Game.Scripts.Shooter
{
    public interface IHealth   
    {
        ulong NetworkObjId { get; }
        void TakeDamage(float damage);
        float GetCurrentHealth();
    }
}
