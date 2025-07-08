using System;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyComponent
	{
		public Observable<GameObject> OnDead => _onDead;

		private Subject<GameObject> _onDead = new();
		[SerializeField]
		private GameObject _gameObject;

		public void Destroy()
		{
			_onDead.OnNext(_gameObject);
			Object.Destroy(_gameObject);
		}
	}
}