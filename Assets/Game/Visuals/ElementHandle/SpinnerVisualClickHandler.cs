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
		private readonly IActiveSpinnerFactory _activeSpinnerFactory;
		private readonly IElementViewDestroyer _destroyer;

		public SpinnerVisualClickHandler(
			Transform activeSpinnerContainer,
			ElementViewsRegistry elementViewsRegistry,
			IActiveSpinnerFactory activeSpinnerFactory,
			IElementViewDestroyer destroyer
		)
		{
			_activeSpinnerContainer = activeSpinnerContainer;
			_elementViewsRegistry = elementViewsRegistry;
			_activeSpinnerFactory = activeSpinnerFactory;
			_destroyer = destroyer;
		}

		public void Handle(HandleResult result)
		{
			if (result is SpinnerStartedHandleResult)
			{
				var element = result.Element;
				ElementView view = _elementViewsRegistry.Registry[element];
				if (view is not SpinnerView spinnerView
				    || element is not Spinner spinner)
				{
					throw new ArgumentException();
				}

				LaunchViewEnlargeAnimation(spinner, spinnerView).Forget();
				_activeSpinnerFactory.CreateActiveSpinner(spinner);
			}
		}

		private async UniTask LaunchViewEnlargeAnimation(MapElement element, SpinnerView spinnerView)
		{
			await DOTween.Sequence(spinnerView.MoveToAnimation(_activeSpinnerContainer.position))
			             .Join(spinnerView.FadeToAnimation(0))
			             .Join(spinnerView.EnlargeAnimation())
			             .ToUniTask();
			OnNoteAnimationFinished(element);
		}

		private void OnNoteAnimationFinished(MapElement element)
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
			return typeof(Spinner);
		}
	}
}