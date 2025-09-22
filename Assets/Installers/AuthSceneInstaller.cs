using System;
using Game.Meta.Authentication;
using Game.SceneSwitch;
using Game.UI;
using ObjectProvide;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
	public sealed class AuthSceneInstaller : LifetimeScope
	{
		[SerializeField]
		private Transform _popupContainer;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<AuthNavigationMediator>(Lifetime.Singleton)
			       .As<IAuthNavigationMediator>();

			builder.RegisterFactory<ILoginWindow, LoginWindowPresenter>(container =>
			{
				return loginWindow =>
				{
					var loginable = container.Resolve<ILoginable>(); // теперь разрешаем при вызове
					var authNavigationMediator = container.Resolve<IAuthNavigationMediator>();
					var mainMenuSceneSwitchable = new MainMenuSceneSwitchable();
					return new LoginWindowPresenter(loginWindow, loginable, authNavigationMediator, mainMenuSceneSwitchable);
				};
			}, Lifetime.Transient);

			builder.Register<LoginWindowFactory>(resolver =>
			{
				return new LoginWindowFactory(
					resolver.Resolve<IObjectProvider>(),
					_popupContainer,
					resolver.Resolve<Func<ILoginWindow, LoginWindowPresenter>>());
			}, Lifetime.Singleton);

			builder.RegisterFactory<IRegisterWindow, RegistrationWindowPresenter>(container =>
			{
				return registerWindow =>
				{
					var registerable = container.Resolve<IRegisterable>();
					var authNavigationMediator = container.Resolve<IAuthNavigationMediator>();
					var mainMenuSceneSwitchable = new MainMenuSceneSwitchable();
					return new RegistrationWindowPresenter(registerWindow, registerable, authNavigationMediator, mainMenuSceneSwitchable);
				};
			}, Lifetime.Transient);

			builder.Register<RegisterWindowFactory>(resolver =>
			{
				return new RegisterWindowFactory(
					resolver.Resolve<IObjectProvider>(),
					_popupContainer,
					resolver.Resolve<Func<IRegisterWindow, RegistrationWindowPresenter>>());
			}, Lifetime.Singleton);

			builder.RegisterFactory<IAuthOptionWindow, AuthOptionWindowPresenter>(container =>
			{
				return authWindow =>
				{
					var authNavigationMediator = container.Resolve<IAuthNavigationMediator>();
					return new AuthOptionWindowPresenter(authWindow, authNavigationMediator /*, loginWindowFactory если конструктор требует */);
				};
			}, Lifetime.Transient);

			builder.Register<AuthOptionWindowFactory>(resolver =>
				new AuthOptionWindowFactory(
					resolver.Resolve<IObjectProvider>(),
					_popupContainer,
					resolver.Resolve<Func<IAuthOptionWindow, AuthOptionWindowPresenter>>()
					), Lifetime.Singleton);

			builder.Register<AuthWindowLauncher>(Lifetime.Singleton)
			       .AsImplementedInterfaces();
		}
	}
}