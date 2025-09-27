using System;
using Audio;
using Beatmaps;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.ElementHandle;
using UnityEngine;

namespace Game.Visuals
{
	public sealed class SingleNoteVisualClickHandler : IVisualClickHandler
	{
		private const string HIT_CLIP_ID = "singleNoteHitSound";
		private const string MISS_CLIP_ID = "missSound";
		private readonly Transform _endPoint;
		private readonly ElementViewsRegistry _elementViewsRegistry;

		private readonly IElementViewDestroyer _destroyer;

		private AudioManager _audioManager;

		public SingleNoteVisualClickHandler(
			Transform endPoint,
			ElementViewsRegistry elementViewsRegistry,
			IElementViewDestroyer destroyer, 
			AudioManager audioManager
			)
		{
			_endPoint = endPoint;
			_elementViewsRegistry = elementViewsRegistry;
			_destroyer = destroyer;
			_audioManager = audioManager;
		}

		public Type GetElementType()
		{
			return typeof(SingleNote);
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

			PlayHitSound();
			if (result is NoteHitHandleResult)
			{
				await DOTween.Sequence(singleNoteView.MoveToAnimation(_endPoint.position))
				             .Join(singleNoteView.FadeToAnimation(0))
				             .ToUniTask();
				OnAnimationEnd(element);
			}
			else if (result is MissHandleResult)
			{
				PlayMissSound();
				DestroyView(element);
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

		private void OnAnimationEnd(MapElement element)
		{
			DestroyView(element);
		}

		private void DestroyView(MapElement element)
		{
			_destroyer.DestroyView(element);
		}
	}
}