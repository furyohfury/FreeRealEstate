using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class CarriableUIComponent
	{
		[SerializeField]
		private CarriableUI _carriableUI;

		public void SetAmount(int current, int needed)
		{
			_carriableUI.SetCount($"{current}/{needed}");
			_carriableUI.SetBar((float)current / needed);
		}
	}
}