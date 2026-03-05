using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SessionParamsStorage", menuName = "Game/SessionParamsStorage")]
    public sealed class SessionParamsStorage : ScriptableObject
    {
        public SessionParams[] SessionParams;
    }
}
