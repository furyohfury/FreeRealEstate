using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
	public sealed class UIInstaller : MonoInstaller
	{
		[SerializeField]
		private TMP_Text _targetCellText;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<TargetCellTextPresenter>()
			         .AsSingle()
			         .WithArguments(_targetCellText);
		}
	}
}