using System;
using R3;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class DestroyCompositeComponent
	{
		public Subject<GameObject> OnMainDestroyed = new();
		public Subject<GameObject> OnSubDestroyed = new();
		
		[SerializeField]
		private GameObject[] _subObjects;
		[SerializeField]
		private GameObject _main;
		private int _subObjectIndex = 0;

		public void Destroy()
		{
			if (_subObjectIndex >= _subObjects.Length)
			{
				DestroyMain();
			}
			else
			{
				DestroySubObject(_subObjectIndex++);
			}
		}

		private void DestroySubObject(int index)
		{
			OnSubDestroyed.OnNext(_subObjects[index]);
			GameObject.Destroy(_subObjects[index]);
		}

		private void DestroyMain()
		{
			OnMainDestroyed.OnNext(_main);
			GameObject.Destroy(_main);
		}
	}
}