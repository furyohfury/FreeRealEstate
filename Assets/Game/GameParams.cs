using UnityEngine;

namespace Game
{
    public sealed class GameParams : Singleton<GameParams>
    {
        [field: SerializeField]
        public SessionParams Params
        {
            get;
            private set;
        }

        public Color[] Colors;
    }
}
