using TriInspector;
using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class PlayerProfileDebugHelper : MonoBehaviour
    {
        [ShowInInspector]
        public string Nickname => _playerProfile.Nickname;
        [Inject]
        private PlayerProfile _playerProfile;
    }
}
