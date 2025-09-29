using System;

namespace Game.SceneSwitch
{
	public interface ISceneSwitchable
	{
		public static Action<string> OnSceneSwitched;
		
		void SwitchScene();
	}
}