using System;
using Beatmaps;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SingleNoteViewFactory : IElementFactory
	{
		private readonly SingleNotePrefabConfig _config;

		public SingleNoteViewFactory(SingleNotePrefabConfig config)
		{
			_config = config;
		}

		public Type GetElementType()
		{
			return typeof(SingleNote);
		}

		public ElementView Spawn(MapElement element, Transform parent)
		{
			if (element is not SingleNote singleNote)
			{
				throw new ArgumentException("Expected single note");
			}

			var view = _config.Views[singleNote.Note];
			return Object.Instantiate(view, parent);
		}
	}
}