using System;
using Game.ElementHandle;
using Game.Scoring;
using R3;
using VContainer.Unity;

namespace Game.Controllers
{
	public sealed class ComboController : IInitializable, IDisposable
	{
		private readonly ScoreSystem _scoreSystem;
		private readonly IHandleResultObservable _handleResultObservable;
		private IDisposable _disposable;

		public ComboController(IHandleResultObservable handleResultObservable, ScoreSystem scoreSystem)
		{
			_handleResultObservable = handleResultObservable;
			_scoreSystem = scoreSystem;
		}

		public void Initialize()
		{
			_disposable = _handleResultObservable.OnElementHandled
			                                     .Subscribe(OnNoteHit);
		}

		private void OnNoteHit(HandleResult result)
		{
			if (result is MissHandleResult)
			{
				_scoreSystem.ResetCombo();
			}
			else
			{
				_scoreSystem.AddCombo();
			}
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}