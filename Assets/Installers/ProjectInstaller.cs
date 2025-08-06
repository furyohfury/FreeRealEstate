using Audio;
using Game.Audio;
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
		}

		private static void InstallObjectProvider(IContainerBuilder builder)
		{
			builder.Register<AddressablesObjectProvider>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
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
	}
}