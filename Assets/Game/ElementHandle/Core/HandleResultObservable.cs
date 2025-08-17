using System.Collections.Generic;
using System.Linq;
using R3;
using VContainer.Unity;

namespace Game.ElementHandle
{
	public sealed class HandleResultObservable : IInitializable, IHandleResultObservable
	{
		public Observable<HandleResult> OnElementHandled => _onElementHandled;
		private Observable<HandleResult> _onElementHandled;
		private readonly IEnumerable<IElementHandleEmitter> _emitters;

		public HandleResultObservable(IEnumerable<IElementHandleEmitter> emitters)
		{
			_emitters = emitters;
		}

		public void Initialize()
		{
			_onElementHandled = Observable.Defer(() =>
				                              _emitters
					                              .Select(e => e.GetStream())
					                              .Merge()
			                              )
			                              // Чтобы одно и то же соединение раздавалось всем подписчикам
			                              .Publish()
			                              .RefCount();
		}
	}
}