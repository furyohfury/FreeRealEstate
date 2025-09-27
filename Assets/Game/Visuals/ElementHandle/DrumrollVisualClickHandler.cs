using System;
using Audio;
using Beatmaps;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class DrumrollVisualClickHandler : IVisualClickHandler
	{
		private const string MISS_CLIP_ID = "missSound";
		private const string HIT_CLIP_ID = "drumrollHitSound";
		private readonly Transform _endPoint;
		private readonly Transform _hitPoint;
		private readonly Transform _container;
		private readonly IPrefabFactory _prefabFactory;
		private readonly IElementViewDestroyer _viewDestroyer;
		private readonly AudioManager _audioManager;

		public DrumrollVisualClickHandler(
			IPrefabFactory prefabFactory,
			Transform endPoint,
			Transform container,
			Transform hitPoint,
			IElementViewDestroyer viewDestroyer,
			AudioManager audioManager
			)
		{
			_endPoint = endPoint;
			_prefabFactory = prefabFactory;
			_hitPoint = hitPoint;
			_viewDestroyer = viewDestroyer;
			_audioManager = audioManager;
			_container = container;
		}

		public Type GetElementType()
		{
			return typeof(Drumroll);
		}

		public async void Handle(HandleResult result)
		{
			if (result is DrumrollHitHandleResult)
			{
				PlayHitSound();
				var noteView = await _prefabFactory.Spawn<DrumrollNoteView>(PrefabsStaticNames.DRUMROLL_NOTE, _container);
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
			else if (result is DrumrollCompleteHandleResult)
			{
				DestroyView(result.Element);
			}
			else if (result is MissHandleResult)
			{
				PlayMissSound();
				DestroyView(result.Element);
			}
		}

		private void PlayHitSound()
		{
			_audioManager.PlaySoundOneShot(HIT_CLIP_ID, AudioOutput.UI).Forget();
		}

		private void PlayMissSound()
		{
			_audioManager.PlaySoundOneShot(MISS_CLIP_ID, AudioOutput.UI).Forget();
		}

		private void OnNoteAnimationFinished(DrumrollNoteView noteView)
		{
			noteView.DestroyView();
		}

		private void DestroyView(MapElement element)
		{
			_viewDestroyer.DestroyView(element);
		}
	}
}