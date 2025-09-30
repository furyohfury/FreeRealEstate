using System.Diagnostics;
using Beatmaps;
using Cysharp.Threading.Tasks;
using FirebaseSystem;
using Game;
using Game.BeatmapBundles;
using Game.BeatmapControl;
using Game.BeatmapLaunch;
using Game.BeatmapRestart;
using Game.BeatmapTime;
using Game.ElementHandle;
using Game.Scoring;
using Game.Visuals;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using Debug = UnityEngine.Debug;

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
		private ElementsVisualsSpawner _elementsVisualsSpawner;
		[Inject]
		private IMapScore _mapScore;
		[Inject]
		private BeatmapPipeline _beatmapPipeline;
		[Inject]
		private IHandleResultObservable _handleResultObservable;
		[Inject]
		private BeatmapRestarter _beatmapRestarter;
		[Inject]
		private FirebaseManager _firebaseManager;

		[SerializeField] [ReadOnly]
		private float _time = 0f;
		[SerializeField] [ReadOnly]
		private int _score = 0;
		[SerializeReference]
		private MapElement _activeMapElement;

		private readonly CompositeDisposable _disposable = new();

		private async void Start()
		{
			_handleResultObservable.OnElementHandled
			                       .Subscribe(result => Debug.Log($"Handled result: {result.Element} with result {result.GetType().Name}"))
			                       .AddTo(_disposable);
		}

		[Button]
		public async UniTask LoginFirst()
		{
			var register = await _firebaseManager.Register("test@gmail.com", "123456", "TestUser");
			if (register is AuthFailure failure)
			{
				var login = await _firebaseManager.SignIn("test@gmail.com", "123456");
			}
		}

		[Button]
		public async UniTask LoginSecond()
		{
			var register = await _firebaseManager.Register("test2@gmail.com", "123456asd", "TestUser2");
			if (register is AuthFailure failure)
			{
				var login = await _firebaseManager.SignIn("test2@gmail.com", "123456asd");
			}
		}
		
		[Button]
		public async UniTask LoginThird()
		{
			var register = await _firebaseManager.Register("test3@gmail.com", "123444asd", "TestUser3");
			if (register is AuthFailure failure)
			{
				var login = await _firebaseManager.SignIn("test3@gmail.com", "123444asd");
			}
		}

		[Button]
		public void RestartMap()
		{
			_beatmapRestarter.Restart();
		}

		[Button]
		public void LaunchSingleNoteMap()
		{
			_beatmapLauncher.Launch(_beatmapBundle, 0);
		}

		[Button]
		public void LaunchDrumrollMap()
		{
			_beatmapLauncher.Launch(_beatmapBundle, 1);
		}

		[Button]
		public void LaunchSpinnerMap()
		{
			_beatmapLauncher.Launch(_beatmapBundle, 2);
		}


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

		[SerializeField]
		private BeatmapBundle _beatmapBundleParsed;
		[Button]
		public void LaunchParsedMap()
		{
			_beatmapLauncher.Launch(_beatmapBundleParsed, 0);
		}

		[Button]
		private void DebugUpdateObservables()
		{
			var sw = new Stopwatch();
			sw.Start();
			Observable.EveryUpdate(UnityFrameProvider.Update)
			          .Take(1)
			          .Subscribe(_ => Debug.Log($"Update = {sw.ElapsedTicks.ToString()}"))
			          .AddTo(_disposable);
			Observable.EveryUpdate(UnityFrameProvider.PreUpdate)
			          .Take(1)
			          .Subscribe(_ => Debug.Log($"PreUpdate = {sw.ElapsedTicks.ToString()}"))
			          .AddTo(_disposable);

			Observable.EveryUpdate(UnityFrameProvider.EarlyUpdate)
			          .Take(1)
			          .Subscribe(_ => Debug.Log($"EarlyUpdate = {sw.ElapsedTicks.ToString()}"))
			          .AddTo(_disposable);
		}

		private void Update()
		{
			UpdateMapTime();
			UpdateScore();
			// UpdateActiveElement();
		}

		// private void UpdateActiveElement()
		// {
		// 	if (_beatmapPipeline.Element.CurrentValue != null)
		// 	{
		// 		_activeMapElement = _beatmapPipeline.Element.CurrentValue;
		// 	}
		// }

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

		private void OnDestroy()
		{
			_disposable.Dispose();
		}
	}
}