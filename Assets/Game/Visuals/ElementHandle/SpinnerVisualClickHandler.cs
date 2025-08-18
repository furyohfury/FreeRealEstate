using System;
using Beatmaps;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class SpinnerVisualClickHandler : IVisualClickHandler
	{
		private readonly Transform _activeSpinnerContainer;
		private readonly ElementViewsRegistry _elementViewsRegistry;

		public SpinnerVisualClickHandler(Transform activeSpinnerContainer, ElementViewsRegistry elementViewsRegistry)
		{
			_activeSpinnerContainer = activeSpinnerContainer;
			_elementViewsRegistry = elementViewsRegistry;
		}

		public void Handle(HandleResult result)
		{
			if (result is SpinnerStartedHandleResult)
			{
				var element = result.Element;
				LaunchViewEnlargeAnimation(element);
			}
		}

		private async void LaunchViewEnlargeAnimation(MapElement element)
		{
			ElementView view = _elementViewsRegistry.ActiveElements[element];
			_elementViewsRegistry.SetInactive(element);
			if (view is not SpinnerView spinnerView)
			{
				throw new ArgumentException();
			}

			await DOTween.Sequence(spinnerView.MoveToAnimation(_activeSpinnerContainer.position))
			             .Join(spinnerView.FadeToAnimation(0))
			             .Join(spinnerView.EnlargeAnimation())
			             .ToUniTask();
			OnNoteAnimationFinished(view);
		}

		private void OnNoteAnimationFinished(ElementView view)
		{
			view.DestroyView();
		}

		public Type GetElementType()
		{
			return typeof(Spinner);
		}
	}
}