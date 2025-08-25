using System;
using R3;
using VContainer.Unity;

namespace Game.BeatmapLaunch
{
	public sealed class BundleServiceController : IInitializable, IDisposable
	{
		private readonly CurrentBundleService _bundleService;
		private readonly BeatmapLauncher _beatmapLauncher;
		private IDisposable _disposable;

		public BundleServiceController(CurrentBundleService bundleService, BeatmapLauncher beatmapLauncher)
		{
			_bundleService = bundleService;
			_beatmapLauncher = beatmapLauncher;
		}

		public void Initialize()
		{
			_disposable = _beatmapLauncher.OnLaunched
			                              .Subscribe(context =>
			                              {
				                              _bundleService.CurrentBundle = context.Bundle;
				                              _bundleService.CurrentVariant = context.SelectedVariant;
			                              });
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}