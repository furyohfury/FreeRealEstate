using System;
using System.Collections.Generic;
using Beatmaps;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SingleNoteViewFactory : IElementFactory, IStartable
	{
		private readonly SingleNotePrefabConfig _config;
		private readonly IObjectProvider _objectProvider;
		private readonly Dictionary<Notes, SingleNoteView> _prefabs = new();

		public SingleNoteViewFactory(SingleNotePrefabConfig config, IObjectProvider objectProvider)
		{
			_config = config;
			_objectProvider = objectProvider;
		}

		public async void Start()
		{
			foreach (var note in _config.ViewIds.Keys)
			{
				string viewId = _config.ViewIds[note];
				SingleNoteView prefab = await _objectProvider.Get<SingleNoteView>(viewId);
				_prefabs.Add(note, prefab);
			}
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

			var view = _prefabs[singleNote.Note];
			return Object.Instantiate(view, parent);
		}
	}
}