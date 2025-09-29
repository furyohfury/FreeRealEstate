using System;
using Audio;
using Cysharp.Threading.Tasks;
using Game.BeatmapControl;
using ObjectProvide;
using UnityEngine;

namespace Game.BeatmapLaunch
{
	public sealed class BeatmapAudioLauncher : IBeatmapLaunchable
	{
		private readonly AudioManager _audioManager;

		public BeatmapAudioLauncher(AudioManager audioManager)
		{
			_audioManager = audioManager;
		}

		public async UniTask Launch(BeatmapLaunchContext context)
		{
			var startTime = context.SelectedVariant.SongStartTimeInSeconds - NotesStaticData.SCROLL_TIME;
			var song = context.Bundle.SongClip;
			if (startTime < 0)
			{
				await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Abs(startTime)));
			}
			
			_audioManager.PlaySound(
				song,
				AudioOutput.Music, 
				startTime: Mathf.Max(0, startTime)
					);
		}
	}
}