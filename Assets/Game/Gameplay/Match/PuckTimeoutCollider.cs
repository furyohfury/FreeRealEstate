using Unity.Netcode;
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
		private float _timer;
		private bool _isOnPlayerSide;
		
		private const float TIMEOUT = 7;

		[Inject]
		public void Construct(MatchSystem matchSystem)
		{
			_matchSystem = matchSystem;
		}

		private void Update()
		{
			if (_isOnPlayerSide == false)
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
