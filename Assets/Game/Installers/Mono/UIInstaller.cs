using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class UIInstaller : MonoInstaller
	{
		[SerializeField] 
		private TextFieldView _pointsTextField;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<ShipPointsPresenter>()
			         .AsSingle()
			         .WithArguments(_pointsTextField);
		}
	}
}