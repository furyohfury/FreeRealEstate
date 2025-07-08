using System;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class IdComponent
	{
		public string ID => _id;
		
		[SerializeField]
		private string _id;
	}
}