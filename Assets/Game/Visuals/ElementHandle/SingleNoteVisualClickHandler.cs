using System;
using Beatmaps;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class SingleNoteVisualClickHandler : IVisualClickHandler
	{
		private readonly Transform _endPoint;
		private readonly ElementViewsRegistry _elementViewsRegistry;
		private readonly IElementViewDestroyer _destroyer;

		public SingleNoteVisualClickHandler(
			Transform endPoint,
			ElementViewsRegistry elementViewsRegistry,
			IElementViewDestroyer destroyer
		)
		{
			_endPoint = endPoint;
			_elementViewsRegistry = elementViewsRegistry;
			_destroyer = destroyer;
		}

		public async void Handle(HandleResult result)
		{
			var element = result.Element;
			if (_elementViewsRegistry.TryGetView(element, out ElementView view) == false)
			{
				Debug.LogError($"Couldn't find view for element in registry. {element.GetType().Name} : {element.HitTime}");
				return;
			}

			if (view is not SingleNoteView singleNoteView)
			{
				throw new ArgumentException();
			}

			if (result is NoteHitHandleResult)
			{
				await DOTween.Sequence(singleNoteView.MoveToAnimation(_endPoint.position))
				             .Join(singleNoteView.FadeToAnimation(0))
				             .ToUniTask();
				OnAnimationEnd(element, view);
			}
			else
			{
				DestroyView(element);
			}
		}

		private void OnAnimationEnd(MapElement element, ElementView view)
		{
			_elementViewsRegistry.RemoveElement(element);
			DestroyView(element);
		}

		private void DestroyView(MapElement element)
		{
			_destroyer.DestroyView(element);
		}

		public Type GetElementType()
		{
			return typeof(SingleNote);
		}
	}
}