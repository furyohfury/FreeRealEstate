using UnityEngine;

namespace Game.Application.Quality
{
    public sealed class QualitySettingsManager : MonoBehaviour
    {
        private void Start()
        {
            AppConfiguration configuration = AppConfigurationProvider.Instance.Configuration;
            string qualityLevelName = configuration.GetQualityLevelName();
            SetQualityByName(qualityLevelName);
        }

        private void SetQualityByName(string qualityName)
        {
            string[] names = QualitySettings.names;

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == qualityName)
                {
                    QualitySettings.SetQualityLevel(i, true);
                    Debug.Log("Set quality level to " + qualityName);

                    return;
                }
            }
        }
    }
}
