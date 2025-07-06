using R3;
using UnityEngine;
using UnityEngine.Playables;

namespace Game
{
	public sealed class IntroCutscenesController : MonoBehaviour
	{
		[SerializeField]
		private PlayableDirector[] _playables;
		private int _activeIndex = 0;

		private readonly CompositeDisposable _disposable = new();

		private void Awake()
		{
			for (int i = 0, count = _playables.Length; i < count; i++)
			{
				var index = i;
				Observable.FromEvent<PlayableDirector>(h => _playables[index].stopped += h,
					          h => _playables[index].stopped -= h)
				          .Subscribe(_ => Switch())
				          .AddTo(_disposable);
			}
		}

		public void Switch()
		{
			Debug.Log("Switching cutscenes");
			if (_activeIndex < _playables.Length - 1)
			{
				Destroy(_playables[_activeIndex].gameObject);
				_playables[++_activeIndex].gameObject.SetActive(true);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}