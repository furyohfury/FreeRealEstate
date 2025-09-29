using Game.SceneSwitch;
using UnityEngine;
using VContainer;

namespace Game.Bootstrap
{
	public sealed class Bootstrap : MonoBehaviour
	{
		private ISceneSwitchable _sceneSwitchable;

		[Inject]
		public void Construct(ISceneSwitchable sceneSwitchable)
		{
			_sceneSwitchable = sceneSwitchable;
		}

		private void Start()
		{
			_sceneSwitchable.SwitchScene();
		}
	}
}