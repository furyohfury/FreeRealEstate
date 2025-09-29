using UnityEngine.AddressableAssets;

namespace Game.SceneSwitch
{
	public sealed class SampleSceneSwitchable : ISceneSwitchable
	{
		public void SwitchScene()
		{
			var scene = Scenes.SampleScene.ToString();
			ISceneSwitchable.OnSceneSwitched?.Invoke(scene);
			Addressables.LoadSceneAsync(scene);
		}
	}
}