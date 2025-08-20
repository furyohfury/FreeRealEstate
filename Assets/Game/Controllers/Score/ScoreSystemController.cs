using System;
using Game.ElementHandle;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.Controllers
{
	public sealed class ScoreSystemController : IInitializable, IDisposable
	{
		private readonly ScoreSystem _scoreSystem;
		private readonly IHandleResultObservable _handleResultObservable;
		private readonly CompositeDisposable _disposable = new();

		public ScoreSystemController(IHandleResultObservable handleResultObservable, ScoreSystem scoreSystem)
		{
			_handleResultObservable = handleResultObservable;
			_scoreSystem = scoreSystem;
		}

		public void Initialize()
		{
			_handleResultObservable.OnElementHandled
			                       .Where(result => result is not MissHandleResult)
			                       .Subscribe(result => _scoreSystem.AddScore(result))
			                       .AddTo(_disposable);

			_handleResultObservable.OnElementHandled
			                       .OfType<HandleResult, MissHandleResult>()
			                       .Subscribe(_ => _scoreSystem.ResetCombo())
			                       .AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}