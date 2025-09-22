using UnityEngine.AddressableAssets;

namespace Game.SceneSwitch
{
	public sealed class MainMenuSceneSwitchable : ISceneSwitchable
	{
		public void SwitchScene()
		{
			Addressables.LoadSceneAsync("MainMenuScene");
		}
	}
}