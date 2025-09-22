using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Game.UI
{
	public sealed class AuthWindowLauncher : IAsyncStartable
	{
		private readonly IAuthNavigationMediator _authNavigationMediator;

		public AuthWindowLauncher(IAuthNavigationMediator authNavigationMediator)
		{
			_authNavigationMediator = authNavigationMediator;
		}

		public async UniTask StartAsync(CancellationToken cancellation = new())
		{
			await _authNavigationMediator.ShowOptions();
		}
	}
}