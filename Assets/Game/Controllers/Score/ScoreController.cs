using System;
using Game.ElementHandle;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.Controllers
{
	public sealed class ScoreController : IInitializable, IDisposable
	{
		private readonly ScoreSystem _scoreSystem;
		private readonly IHandleResultObservable _handleResultObservable;
		private IDisposable _disposable;

		public ScoreController(IHandleResultObservable handleResultObservable, ScoreSystem scoreSystem)
		{
			_handleResultObservable = handleResultObservable;
			_scoreSystem = scoreSystem;
		}

		public void Initialize()
		{
			_disposable = _handleResultObservable.OnElementHandled
			                                     .Where(result => result is not MissHandleResult)
			                                     .Subscribe(OnNoteHit);
		}

		private void OnNoteHit(HandleResult result)
		{
			_scoreSystem.AddScore(result);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}