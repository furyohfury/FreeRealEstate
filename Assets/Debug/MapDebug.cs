using System;
using Game;
using Game.BeatmapControl;
using Game.Scoring;
using Game.SongMapTime;
using Game.Visuals;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameDebug
{
	public class MapDebug : MonoBehaviour
	{
		[SerializeField]
		private BeatmapDebug _map;
		[Inject]
		private IMapTime _mapTime;
		[Inject]
		private BeatmapLauncher _beatmapLauncher;
		[Inject]
		private NotesVisualSystem _notesVisualSystem;
		[Inject]
		private MapScore _mapScore;
		[Inject]
		private BeatmapPipeline _beatmapPipeline;

		[SerializeField] [ReadOnly]
		private float _time = 0f;
		[SerializeField] [ReadOnly]
		private int _score = 0;
		[SerializeField] [ReadOnly]
		private int _activeIndex = 0;

		[Button]
		public void SetMap()
		{
			_beatmapPipeline.SetMap(_map);
		}

		[Button]
		public void LaunchMap()
		{
			_beatmapLauncher.LaunchActiveMap();
			_notesVisualSystem.LaunchMap(_map);
		}

		[Button]
		private void RestartMap()
		{
			_beatmapPipeline.RestartMap();
		}

		private void Update()
		{
			UpdateMapTime();
			UpdateScore();
		}

		private void UpdateMapTime()
		{
			if (_mapTime != null)
			{
				_time = _mapTime.GetMapTimeInSeconds();
			}
		}

		private void UpdateScore()
		{
			if (_mapScore != null)
			{
				_score = _mapScore.Score.CurrentValue;
			}
		}
	}
}