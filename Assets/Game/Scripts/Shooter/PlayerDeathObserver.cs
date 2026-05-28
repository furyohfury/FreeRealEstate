using System;
using Game.Scripts.Shooter;
using Zenject;

namespace Game
{
    public sealed class PlayerDeathObserver : IInitializable, IDisposable
    {
        private readonly ScoreSystem _scoreSystem;
        private readonly DealDamageSystem _dealDamageSystem;
        private readonly SessionSystem _sessionSystem;

        public PlayerDeathObserver(ScoreSystem scoreSystem, DealDamageSystem dealDamageSystem, SessionSystem sessionSystem)
        {
            _scoreSystem = scoreSystem;
            _dealDamageSystem = dealDamageSystem;
            _sessionSystem = sessionSystem;
        }

        public void Initialize()
        {
            _dealDamageSystem.OnKill += DealDamageSystemOnOnKill;
        }

        private void DealDamageSystemOnOnKill(KillEvent obj)
        {
            _sessionSystem.LaunchNextRound();
            _scoreSystem.ScoreKillPoints(obj.KillerNetworkObjectId);
        }

        public void Dispose()
        {
            _dealDamageSystem.OnKill -= DealDamageSystemOnOnKill;
        }
    }
}
