using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class Cell
	{
		public string ID => _id;

		public Sprite Icon => _icon;

		[SerializeField]
		private string _id;
		[SerializeField]
		private Sprite _icon;
	}
}