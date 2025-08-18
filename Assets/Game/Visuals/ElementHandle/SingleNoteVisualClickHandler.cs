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

		public SingleNoteVisualClickHandler(Transform endPoint, ElementViewsRegistry elementViewsRegistry)
		{
			_endPoint = endPoint;
			_elementViewsRegistry = elementViewsRegistry;
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


			_elementViewsRegistry.SetInactive(element);
			if (result is NoteHitHandleResult)
			{
				await DOTween.Sequence(singleNoteView.MoveToAnimation(_endPoint.position))
				       .Join(singleNoteView.FadeToAnimation(0))
				       .ToUniTask();
				OnAnimationEnd(element, view);
			}
			else
			{
				DestroyView(view);
			}
		}

		private void OnAnimationEnd(MapElement element, ElementView view)
		{
			_elementViewsRegistry.RemoveElement(element);
			DestroyView(view);
		}

		private void DestroyView(ElementView view)
		{
			view.DestroyView();
		}

		public Type GetElementType()
		{
			return typeof(SingleNote);
		}
	}
}