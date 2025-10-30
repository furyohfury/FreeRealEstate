using Gameplay;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class MatchSystemInstaller : MonoInstaller
	{
		[SerializeField]
		private Collider _playerOneScoreZoneCollider;
		[SerializeField]
		private Collider _playerTwoScoreZoneCollider;
		[SerializeField]
		private MatchSettings _matchSettings;

		public override void InstallBindings()
		{
			Container.Bind<GoalObservable>()
			         .FromInstance(new GoalObservable(_playerOneScoreZoneCollider, _playerTwoScoreZoneCollider))
			         .AsSingle();

			Container.Bind<MatchSettings>()
			         .FromInstance(_matchSettings)
			         .AsSingle();

			Container.Bind<GameFinisher>()
			         .AsSingle();

			Container.BindInterfacesAndSelfTo<MatchSystem>()
			         .AsSingle();

			Container.BindInterfacesTo<PlayerDisconnectObserver>()
			         .AsSingle();
		}
	}
}