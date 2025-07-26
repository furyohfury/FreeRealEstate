using System;
using UnityEngine;

namespace Beatmaps
{
	[Serializable]
	public sealed class SingleNote : MapElement
	{
		public Notes Note => _note;
		[SerializeField]
		private Notes _note;

		public SingleNote(float timeSeconds, Notes note) : base(timeSeconds)
		{
			_note = note;
		}
	}
}