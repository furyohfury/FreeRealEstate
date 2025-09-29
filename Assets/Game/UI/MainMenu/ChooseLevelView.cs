using System.Collections.Generic;
using Game.BeatmapBundles;
using Game.BeatmapLaunch;
using Game.SceneSwitch;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI.MainMenu
{
	public sealed class ChooseLevelView : SerializedMonoBehaviour
	{
		[SerializeField]
		private BeatmapBundle _beatmapBundle;
		[SerializeField]
		private Dictionary<Button, int> _buttons;

		private CurrentBundleService _bundleService;
		private ISceneSwitchable _sceneSwitchable = new SampleSceneSwitchable();

		[Inject]
		public void Construct(CurrentBundleService bundleService)
		{
			_bundleService = bundleService;
		}

		private void OnEnable()
		{
			foreach (var button in _buttons.Keys)
			{
				button.OnClickAsObservable()
				      .Subscribe(_ =>
				      {
					      _bundleService.CurrentBundle = _beatmapBundle;
					      var index = _buttons[button];
					      _bundleService.CurrentVariant = _beatmapBundle.BeatmapsVariants[index];
					      _sceneSwitchable.SwitchScene();
				      })
				      .AddTo(this);
			}
		}
	}
}