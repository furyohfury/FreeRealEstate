using UnityEngine.AddressableAssets;

namespace Game.SceneSwitch
{
	public sealed class AuthSceneSwitchable : ISceneSwitchable
	{
		public void SwitchScene()
		{
			var scene = Scenes.AuthScene.ToString();
			ISceneSwitchable.OnSceneSwitched?.Invoke(scene);
			Addressables.LoadSceneAsync(scene);
		}
	}
}