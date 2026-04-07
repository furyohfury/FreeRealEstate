using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class GameParamsService : Singleton<GameParamsService>
    {
        [field: FormerlySerializedAs("<SessionParams>k__BackingField")]
        [field: SerializeField]
        public SessionParams SessionParams { get; set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }
}
