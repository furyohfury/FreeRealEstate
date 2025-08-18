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
		private readonly IActiveSpinnerController _activeSpinnerController;

		public SpinnerVisualClickHandler(
			Transform activeSpinnerContainer,
			ElementViewsRegistry elementViewsRegistry,
			IActiveSpinnerController activeSpinnerController
		)
		{
			_activeSpinnerContainer = activeSpinnerContainer;
			_elementViewsRegistry = elementViewsRegistry;
			_activeSpinnerController = activeSpinnerController;
		}

		public void Handle(HandleResult result)
		{
			if (result is SpinnerStartedHandleResult)
			{
				var element = result.Element;
				ElementView view = _elementViewsRegistry.ActiveElements[element];
				_elementViewsRegistry.SetInactive(element);
				if (view is not SpinnerView spinnerView
				    || element is not Spinner spinner)
				{
					throw new ArgumentException();
				}

				LaunchViewEnlargeAnimation(spinner, spinnerView).Forget();
				_activeSpinnerController.CreateActiveSpinnerView(spinner);
			}
		}

		private async UniTask LaunchViewEnlargeAnimation(MapElement element, SpinnerView spinnerView)
		{
			await DOTween.Sequence(spinnerView.MoveToAnimation(_activeSpinnerContainer.position))
			             .Join(spinnerView.FadeToAnimation(0))
			             .Join(spinnerView.EnlargeAnimation())
			             .ToUniTask();
			OnNoteAnimationFinished(element, spinnerView);
		}

		private void OnNoteAnimationFinished(MapElement element, ElementView view)
		{
			_elementViewsRegistry.RemoveElement(element);
			view.DestroyView();
		}

		public Type GetElementType()
		{
			return typeof(Spinner);
		}
	}
}