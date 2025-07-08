using System;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class PikminService : IInitializable, IDisposable
	{
		public Observable<int> PikminCount;
		public int CurrentCount => _pikmins.Count;
		public static int CurrentMaxCount = 0;
		public const int TOTAL_MAX_COUNT = 10;

		private readonly ObservableHashSet<GameObject> _pikmins = new();
		private readonly CompositeDisposable _disposable = new();

		public void Initialize()
		{
			PikminCount = _pikmins.ObserveCountChanged(true);
			GameObject[] pikmins = GameObject.FindGameObjectsWithTag("Pikmin");
			for (int i = 0, count = pikmins.Length; i < count; i++)
			{
				AddPikmin(pikmins[i]);
			}

		}

		public void AddPikmin(GameObject pikmin)
		{
			if (pikmin.TryGetComponent(out CommonPikmin commonPikmin) == false)
			{
				throw new ArgumentException("No pikmin component");
			}

			if (_pikmins.Add(pikmin) == false)
			{
				return;
			}

			UpdateCurrentMaxCount();

			commonPikmin.OnDead
			            .Subscribe(OnPikminDead)
			            .AddTo(_disposable);
		}

		private void OnPikminDead(GameObject pikmin)
		{
			_pikmins.Remove(pikmin);
			UpdateCurrentMaxCount();
		}

		private void UpdateCurrentMaxCount()
		{
			CurrentMaxCount = Mathf.Max(CurrentMaxCount, CurrentCount);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}