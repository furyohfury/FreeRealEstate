using R3;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	[RequireComponent(typeof(Collider))]
	public sealed class PuckTimeoutCollider : MonoBehaviour
	{
		[SerializeField]
		private Player _player;
		private MatchSystem _matchSystem;
		private Score _score;
		private float _timer;
		private bool _isOnPlayerSide;
		private bool _isMatchActive;

		private const float TIMEOUT = 7;

		[Inject]
		public void Construct(MatchSystem matchSystem, Score score)
		{
			_matchSystem = matchSystem;
			_score = score;
		}

		private void Start()
		{
			_matchSystem.OnMatchStarted
			            .Subscribe(_ => _isMatchActive = true)
			            .AddTo(this);

			_matchSystem.OnMatchWon
			            .Subscribe(_ => _isMatchActive = false)
			            .AddTo(this);

			_score.GetScoreObservable(Player.Two)
			      .Merge(_score.GetScoreObservable(Player.One))
			      .Subscribe(_ => _timer = 0f)
			      .AddTo(this);
		}

		private void Update()
		{
			if (_isMatchActive == false ||
			    _isOnPlayerSide == false)
			{
				return;
			}

			_timer += Time.deltaTime;

			if (_timer >= TIMEOUT)
			{
				_matchSystem.ScoreGoal(_player == Player.One
					? Player.Two
					: Player.One);

				_timer = 0f;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Puck _))
			{
				_isOnPlayerSide = true;
				_timer = 0f;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out Puck _))
			{
				_isOnPlayerSide = false;
			}
		}
	}
}