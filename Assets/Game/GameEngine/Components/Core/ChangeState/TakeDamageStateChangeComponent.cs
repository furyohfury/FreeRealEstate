using System;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public sealed class TakeDamageStateChangeComponent
	{
		[SerializeField]
		private GameObject[] _stateObjects;
		private int _stateObjectIndex = 0;

		public void ChangeState()
		{
			if (_stateObjectIndex >= _stateObjects.Length - 1)
			{
				return;
			}

			_stateObjects[_stateObjectIndex++].SetActive(false);
			_stateObjects[_stateObjectIndex].SetActive(true);
		}
	}
}