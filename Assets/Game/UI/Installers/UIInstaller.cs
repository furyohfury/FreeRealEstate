using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public sealed class UIInstaller : MonoInstaller
	{
		[SerializeField]
		private TMP_Text _targetCellText;
		[SerializeField] 
		private Button _restartButton;
		[SerializeField] 
		private Image _shadowingImage;
		[SerializeField] 
		private Image _loadingScreen;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<TargetCellTextPresenter>()
			         .AsSingle()
			         .WithArguments(_targetCellText);

			Container.BindInterfacesAndSelfTo<LevelEndObserver>()
			         .AsSingle()
			         .WithArguments(_restartButton, _shadowingImage);

			Container.BindInterfacesAndSelfTo<LevelRestarter>()
			         .AsSingle()
			         .WithArguments(_loadingScreen);
		}
	}
}