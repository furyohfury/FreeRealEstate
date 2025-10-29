using System.Collections.Generic;
using R3;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class Score : NetworkBehaviour
	{
		private readonly NetworkVariable<int> _playerOneScore = new();
		private readonly NetworkVariable<int> _playerTwoScore = new();
		private Dictionary<Player, ReactiveProperty<int>> _scoreObservables;

		private void Awake()
		{
			_scoreObservables = new Dictionary<Player, ReactiveProperty<int>>
			                    {
				                    { Player.One, new ReactiveProperty<int>(0) }, { Player.Two, new ReactiveProperty<int>(0) }
			                    };
		}

		public override void OnNetworkSpawn()
		{
			_playerOneScore.OnValueChanged += OnP1ValueChanged;
			_playerTwoScore.OnValueChanged += OnP2ValueChanged;
		}

		private void OnP1ValueChanged(int previousvalue, int newvalue)
		{
			_scoreObservables[Player.One].Value = newvalue;
			Debug.Log($"Score is {_playerOneScore.Value}:{_playerTwoScore.Value}");
		}

		private void OnP2ValueChanged(int previousvalue, int newvalue)
		{
			_scoreObservables[Player.Two].Value = newvalue;
			Debug.Log($"Score is {_playerOneScore.Value}:{_playerTwoScore.Value}");
		}

		public Observable<int> GetScoreObservable(Player player)
		{
			return _scoreObservables[player];
		}

		public void AddPoint(Player player)
		{
			if (!IsHost) return;

			if (player == Player.One)
			{
				_playerOneScore.Value++;
			}
			else
			{
				_playerTwoScore.Value++;
			}
		}

		public int GetScore(Player player)
		{
			return _scoreObservables[player].CurrentValue;
		}

		public void ResetScore()
		{
			_playerOneScore.Value = 0;
			_playerTwoScore.Value = 0;
		}

		public override void OnDestroy()
		{
			_playerOneScore.OnValueChanged -= OnP1ValueChanged;
			_playerTwoScore.OnValueChanged -= OnP2ValueChanged;
		}
	}
}