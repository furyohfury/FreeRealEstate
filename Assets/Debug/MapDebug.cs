using System;
using Game;
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
		private SongMapDebug _map;
		[Inject]
		private ActiveMapService _activeMapService;
		[Inject]
		private IMapTime _mapTime;
		[Inject]
		private BeatmapLauncher _beatmapLauncher;
		[Inject]
		private NotesVisualSystem _notesVisualSystem;
		[Inject]
		private MapScore _mapScore;
		[Inject]
		private ActiveElementService _activeElementService;
		[Inject]
		private ActiveElementIndexService _activeElementIndexService;

		[SerializeField] [ReadOnly]
		private float _time = 0f;
		[SerializeField] [ReadOnly]
		private int _score = 0;
		[SerializeField] [ReadOnly]
		private int _activeIndex = 0;

		[Button]
		public void SetMap()
		{
			_activeMapService.SetMap(_map);
		}

		[Button]
		public void LaunchMap()
		{
			_beatmapLauncher.LaunchActiveMap();
			_notesVisualSystem.LaunchMap(_map);
		}

		// [Button]
		// private void Reset()
		// {
		// 	_mapTime.Reset();
		// }

		private void Update()
		{
			UpdateMapTime();
			UpdateScore();
			UpdateActiveIndex();
		}

		private void UpdateActiveIndex()
		{
			if (_activeElementIndexService != null)
			{
				_activeIndex = _activeElementIndexService.ActiveIndex;
			}
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