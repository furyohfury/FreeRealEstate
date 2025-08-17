using System;
using Beatmaps;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class SingleNoteVisualClickHandler : IVisualClickHandler
	{
		private readonly Transform _endPoint;

		public SingleNoteVisualClickHandler(Transform endPoint)
		{
			_endPoint = endPoint;
		}

		public void Handle(ElementView view, HandleResult result)
		{
			if (view is not SingleNoteView singleNoteView)
			{
				throw new ArgumentException();
			}

			if (result is NoteHitHandleResult)
			{
				DOTween.Sequence()
				       .Append(DOTween.To(
					       view.GetPosition,
					       view.Move,
					       _endPoint.position,
					       0.5f
				       ))
				       .Join(DOTween.To(
					       () => singleNoteView.Alpha,
					       alpha => singleNoteView.Alpha = alpha,
					       0,
					       0.5f
				       ))
				       .AppendCallback(OnAnimationEnd(view));
			}
			else
			{
				view.DestroyView();
			}
		}

		private static TweenCallback OnAnimationEnd(ElementView view)
		{
			return view.DestroyView;
		}

		public Type GetElementType()
		{
			return typeof(SingleNote);
		}
	}
}