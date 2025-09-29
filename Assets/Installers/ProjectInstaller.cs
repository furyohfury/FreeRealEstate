using Audio;
using FirebaseSystem;
using Game;
using Game.Audio;
using Game.BeatmapLaunch;
using Game.Leaderboard;
using Game.Meta.Authentication;
using Game.Visuals;
using ObjectProvide;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public sealed class ProjectInstaller : LifetimeScope
	{
		[SerializeField]
		private AudioLibrary _audioLibrary;
		[SerializeField]
		private AudioLayerSetting _audioLayerSetting;
		[SerializeField]
		private AudioMixer _audioMixer;

		protected override void Configure(IContainerBuilder builder)
		{
			InstallObjectProvider(builder);
			InstallAudioSystem(builder);
			RegisterFirebaseSystems(builder);
			RegisterLeaderboardSystems(builder);
			RegisterElementTimeoutHelper(builder);
			builder.Register<CurrentBundleService>(Lifetime.Singleton);
		}

		private static void InstallObjectProvider(IContainerBuilder builder)
		{
			builder.Register<AddressablesObjectProvider>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<AddressablesPrefabFactory>(Lifetime.Singleton)
			       .As<IPrefabFactory>();
		}

		private void InstallAudioSystem(IContainerBuilder builder)
		{
			builder.RegisterInstance<AudioLibrary>(_audioLibrary);
			builder.RegisterInstance<AudioLayerSetting>(_audioLayerSetting);
			builder.RegisterInstance<AudioMixer>(_audioMixer);
			builder.Register<IAudioClipProvider, AddressableClipProvider>(Lifetime.Singleton);
			builder.Register<AudioManager>(Lifetime.Singleton)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}

		private void RegisterFirebaseSystems(IContainerBuilder builder)
		{
			builder.Register<FirebaseManager>(Lifetime.Singleton);

			builder.Register<FirebaseAuthenticationAdapter>(Lifetime.Singleton)
			       .AsImplementedInterfaces();

			builder.Register<FirebaseScoreLoader>(Lifetime.Singleton)
			       .As<IScoreLoader>();
			builder.Register<FirebaseScoreSender>(Lifetime.Singleton)
			       .As<IScoreSender>();
		}

		private static void RegisterLeaderboardSystems(IContainerBuilder builder)
		{
			builder.Register<LeaderboardDataTransferer>(Lifetime.Singleton);
		}

		private static void RegisterElementTimeoutHelper(IContainerBuilder builder)
		{
			builder.Register<IElementTimeoutCalculator, SingleNoteTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<IElementTimeoutCalculator, SpinnerTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<IElementTimeoutCalculator, DrumrollTimeoutCalculator>(Lifetime.Singleton);
			builder.Register<ElementTimeoutHelper>(Lifetime.Singleton);
		}
	}
}