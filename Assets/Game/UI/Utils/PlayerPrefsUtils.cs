using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.UI
{
	public class PlayerPrefsUtils : MonoBehaviour
	{
		private const string AUDIO_SETTINGS_KEY = "audio_settings";
		
		[Button]
		public void ClearAll()
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}

		[Button]
		public void PrintVolumeSettings()
		{
			var prefs = PlayerPrefs.GetString(AUDIO_SETTINGS_KEY);
			Debug.Log(prefs);
		}
	}
}