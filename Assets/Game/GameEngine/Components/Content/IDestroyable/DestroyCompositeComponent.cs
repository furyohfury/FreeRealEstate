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
		private GameObject _subObject;
		[SerializeField]
		private GameObject _main;

		public void Destroy()
		{
			if (_subObject != null)
			{
				OnSubDestroyed.OnNext(_subObject);
				GameObject.Destroy(_subObject);
			}
			else
			{
				OnMainDestroyed.OnNext(_main);
				GameObject.Destroy(_main);
			}
		}
	}
}