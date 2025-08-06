using Beatmaps;
using Game;
using Game.BeatmapBundles;
using Game.BeatmapControl;
using Game.BeatmapLaunch;
using Game.Scoring;
using Game.SongMapTime;
using Game.Visuals;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameDebug
{
	public class MapDebug : MonoBehaviour
	{
		[SerializeField]
		private BeatmapBundle _beatmapBundle;
		[SerializeField]
		private BeatmapDebug _map;
		[SerializeField]
		private BeatmapSpinnerDebug _spinnerDebugMap;
		[SerializeField]
		private BeatmapDrumrollDebug _beatmapDrumrollDebug;
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
		[Inject]
		private InputReader _inputReader;

		[SerializeField] [ReadOnly]
		private float _time = 0f;
		[SerializeField] [ReadOnly]
		private int _score = 0;
		[SerializeReference]
		private MapElement _activeMapElement;

		[Button]
		public void LaunchSingleNoteMap()
		{
			_beatmapLauncher.Launch(_beatmapBundle, 0);
		}

		private readonly SerialDisposable _serialDisposable = new();

		// [Button]
		// public async void LaunchDrumrollAutoMap()
		// {
		// 	_beatmapPipeline.SetMap(_beatmapDrumrollDebug);
		// 	_beatmapTimeLauncher.Launch();
		// 	_notesVisualSystem.LaunchMap(_beatmapDrumrollDebug);
		// 	await UniTask.Delay(5000);
		// 	_inputReader.OnTestNote(Notes.Blue);
		// 	_serialDisposable.Disposable = Observable.Interval(TimeSpan.FromSeconds(0.25))
		// 	                                         .Subscribe(_ => _inputReader.OnTestNote(Notes.Blue));
		// }

		[Button]
		private void RestartMap()
		{
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