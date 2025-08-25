using System;
using Beatmaps;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class DrumrollVisualClickHandler : IVisualClickHandler
	{
		private readonly Transform _endPoint;
		private readonly Transform _hitPoint;
		private readonly Transform _container;
		private readonly PrefabFactory<DrumrollNoteView> _prefabFactory;
		private readonly IElementViewDestroyer _viewDestroyer;

		public DrumrollVisualClickHandler(
			PrefabFactory<DrumrollNoteView> prefabFactory,
			Transform endPoint,
			Transform container,
			Transform hitPoint,
			IElementViewDestroyer viewDestroyer
		)
		{
			_endPoint = endPoint;
			_prefabFactory = prefabFactory;
			_hitPoint = hitPoint;
			_viewDestroyer = viewDestroyer;
			_container = container;
		}

		public void Handle(HandleResult result)
		{
			if (result is DrumrollHitHandleResult)
			{
				var noteView = _prefabFactory.Spawn(_container);
				noteView.Move(_hitPoint.position);

				DOTween.Sequence()
				       .Append(DOTween.To(
					       () => noteView.GetPosition(),
					       pos => noteView.Move(pos),
					       _endPoint.position,
					       0.5f
				       ))
				       .Join(DOTween.To(
					       () => noteView.Alpha,
					       alpha => noteView.Alpha = alpha,
					       0,
					       0.5f
				       ))
				       .AppendCallback(() => OnNoteAnimationFinished(noteView));
			}
			else if (result is MissHandleResult or DrumrollCompleteHandleResult)
			{
				DestroyView(result.Element);
			}
		}

		private void OnNoteAnimationFinished(DrumrollNoteView noteView)
		{
			noteView.DestroyView();
		}

		private void DestroyView(MapElement element)
		{
			_viewDestroyer.DestroyView(element);
		}

		public Type GetElementType()
		{
			return typeof(Drumroll);
		}
	}
}