using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyComponent
	{
		[SerializeField]
		private GameObject _gameObject;

		public void Destroy()
		{
			GameObject.Destroy(_gameObject);
		}
	}
}