using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(ScoreSettingsConfig), menuName = "Game/" + nameof(ScoreSettingsConfig))]
    public sealed class ScoreSettingsConfig : ScriptableObject
    {
        public int KillPoints = 1;
    }
}
