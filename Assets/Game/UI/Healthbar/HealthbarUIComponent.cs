using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[Serializable]
	public class HealthbarUIComponent
	{
		[SerializeField]
		private HealthBar _healthBar;

		public void SetRatio(float ratio)
		{
			_healthBar.SetBar(ratio);
		}

		public void Hide()
		{
			_healthBar.gameObject.SetActive(false);
		}
	}
}