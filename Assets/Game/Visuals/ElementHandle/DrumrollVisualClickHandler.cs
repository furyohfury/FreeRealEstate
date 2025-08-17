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
		private readonly Transform _container;
		private readonly PrefabFactory<DrumrollNoteView> _prefabFactory;

		public DrumrollVisualClickHandler(Transform endPoint, Transform container, PrefabFactory<DrumrollNoteView> prefabFactory)
		{
			_endPoint = endPoint;
			_prefabFactory = prefabFactory;
			_container = container;
		}

		public void Handle(ElementView view, HandleResult result)
		{
			if (result is not DrumrollHitHandleResult)
			{
				return;
			}

			var noteView = _prefabFactory.Spawn(_container);

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
			       .AppendCallback(view.DestroyView);
		}

		public Type GetElementType()
		{
			return typeof(Drumroll);
		}
	}
}