using System;
using System.Collections.Generic;
using System.Threading;
using Beatmaps;
using Cysharp.Threading.Tasks;
using ObjectProvide;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Visuals
{
	public sealed class SingleNoteViewFactory : IElementFactory, IAsyncStartable
	{
		private readonly IObjectProvider _objectProvider;
		private readonly Dictionary<Notes, SingleNoteView> _prefabs = new();
		
		private const string BLUE_NOTE_ID = "BlueNotePrefab";
		private const string RED_NOTE_ID = "RedNotePrefab";

		public SingleNoteViewFactory(IObjectProvider objectProvider)
		{
			_objectProvider = objectProvider;
		}

		public async UniTask StartAsync(CancellationToken cancellation = new())
		{
			_prefabs[Notes.Blue] = await _objectProvider.Get<SingleNoteView>(BLUE_NOTE_ID);
			_prefabs[Notes.Red] = await _objectProvider.Get<SingleNoteView>(RED_NOTE_ID);
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