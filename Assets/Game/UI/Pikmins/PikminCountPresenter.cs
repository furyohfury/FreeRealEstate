using System;
using R3;
using Zenject;

namespace Game
{
	public sealed class PikminCountPresenter : IInitializable, IDisposable
	{
		private readonly TextFieldView _pikminCountView;
		private readonly PikminService _pikminService;
		private readonly CompositeDisposable _disposable = new();

		public PikminCountPresenter(TextFieldView pikminCountView, PikminService pikminService)
		{
			_pikminCountView = pikminCountView;
			_pikminService = pikminService;
		}

		public void Initialize()
		{
			_pikminService.PikminCount
			              .Subscribe(count =>
				              _pikminCountView.SetText($"{count.ToString()}/{PikminService.TOTAL_MAX_COUNT.ToString()}"))
			              .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}