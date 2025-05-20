using UnityEngine;
using Zenject;

namespace Game
{
	[CreateAssetMenu(fileName = "ShipSystemsInstaller", menuName = "Create installer/ShipSystemsInstaller")]
	public sealed class ShipSystemsInstaller : ScriptableObjectInstaller
	{
		[SerializeField] 
		private ConsumablesValuesConfig _consumableValuesConfig;

		public override void InstallBindings()
		{
			Container.Bind<ShipPoints>()
			         .AsSingle();

			Container.Bind<ConsumablesValuesConfig>()
			         .FromInstance(_consumableValuesConfig)
			         .AsSingle();
		}
	}
}