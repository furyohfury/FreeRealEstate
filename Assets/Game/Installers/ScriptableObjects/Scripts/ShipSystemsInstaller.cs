using UnityEngine;
using Zenject;

namespace Game
{
	[CreateAssetMenu(fileName = "ShipSystemsInstaller", menuName = "Create installer/ShipSystemsInstaller")]
	public sealed class ShipSystemsInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<ShipPoints>()
			         .AsSingle();
		}
	}
}