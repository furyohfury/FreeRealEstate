using UnityEngine;

namespace Game
{
    public sealed class GameParamsService : Singleton<GameParamsService>
    {
        [field: SerializeField]
        public SessionParams SessionParams { get; set; }
    }
}
