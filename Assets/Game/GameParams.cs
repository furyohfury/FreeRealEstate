using UnityEngine;

namespace Game
{
    public sealed class GameParams : MonoBehaviour
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
