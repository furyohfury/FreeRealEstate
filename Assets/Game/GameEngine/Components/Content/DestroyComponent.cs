using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyComponent : IComponent
	{
		[SerializeField]
		private GameObject _gameObject;

		public void Destroy()
		{
			Object.Destroy(_gameObject);
		}
	}
}