using Game.ElementHandle;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
	public sealed class TimeoutHandleResultStrategy : IHandleResultStrategy, IInitializable
	{
		private readonly IElementTimeoutObservable _elementTimeoutObservable;
		private readonly Subject<HandleResult> _subject = new();

		public TimeoutHandleResultStrategy(IElementTimeoutObservable elementTimeoutObservable)
		{
			_elementTimeoutObservable = elementTimeoutObservable;
		}

		public void Initialize()
		{
			_elementTimeoutObservable.OnTimeout
			                         .Select(element => (HandleResult)new MissHandleResult(element))
			                         .Subscribe(_subject.AsObserver());

			// _elementTimeoutObservable.OnTimeout.Subscribe(_ => Debug.Log("From _elementTimeoutObservable"));
			// _subject.Subscribe(_ => Debug.Log("Onelementtimeout"));
		}

		public Observable<HandleResult> GetStream()
		{
			return _subject;
		}
	}
}