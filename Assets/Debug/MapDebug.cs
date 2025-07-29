using Beatmaps;
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
		[SerializeField]
		private BeatmapSpinnerDebug _spinnerDebugMap;
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
		[SerializeReference]
		private MapElement _activeMapElement;
		
		[Button]
		public void LaunchSingleNoteMap()
		{
			_beatmapPipeline.SetMap(_map);
			_beatmapLauncher.LaunchActiveMap();
			_notesVisualSystem.LaunchMap(_map);
		}

		[Button]
		public void LaunchSpinnerMap()
		{
			_beatmapPipeline.SetMap(_spinnerDebugMap);
			_beatmapLauncher.LaunchActiveMap();
			_notesVisualSystem.LaunchMap(_spinnerDebugMap);
		}

		[Button]
		private void RestartMap()
		{
			_mapTime.Reset();
			_beatmapPipeline.RestartMap();
		}

		private void Update()
		{
			UpdateMapTime();
			UpdateScore();
			UpdateActiveElement();
		}

		private void UpdateActiveElement()
		{
			if (_beatmapPipeline.Element.CurrentValue != null)
			{
				_activeMapElement = _beatmapPipeline.Element.CurrentValue;
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