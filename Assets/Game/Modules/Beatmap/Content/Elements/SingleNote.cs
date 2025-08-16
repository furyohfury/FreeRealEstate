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

		public SingleNote(float hitTime, Notes note) : base(hitTime)
		{
			_note = note;
		}

		public SingleNote()
		{
		}
	}
}