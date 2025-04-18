using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class BundlesLoopController : IInitializable, IDisposable
	{
		private readonly ActiveBundleService _activeBundleService;

		private readonly CompositeDisposable _disposable = new();

		public BundlesLoopController(ActiveBundleService activeBundleService)
		{
			_activeBundleService = activeBundleService;
		}

		public void Initialize()
		{
			_activeBundleService.OnLevelEnded
			                   .Subscribe(_ => OnLevelEnded())
			                   .AddTo(_disposable);
		}

		private void OnLevelEnded()
		{
			LoopLevel();
		}

		private void LoopLevel()
		{
			_activeBundleService.Reset();
		}

		public void Dispose()
		{
			_disposable.Clear();
		}
	}
}