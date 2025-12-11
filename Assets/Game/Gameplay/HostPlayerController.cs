using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class HostPlayerController : NetworkBehaviour
	{
		[SerializeField]
		private MoveComponent _moveComponent;
		[SerializeField]
		private LayerMask _groundLayer;
		private bool _isControlling;
		private BoxCollider _collider;

		public override void OnNetworkSpawn()
		{
			_collider = FindObjectsByType<PlayzoneCollider>(FindObjectsSortMode.None)
			            .Single(collider => collider.Player == Player.One)
			            .BoxCollider;
		}

		public void ResetInput()
		{
			_isControlling = false;
		}

		private void Update()
		{
			// Только локальный игрок обрабатывает ввод
			if (!IsOwner)
			{
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				_isControlling = true;
			}

			if (Input.GetMouseButtonUp(0))
			{
				_isControlling = false;
			}

			if (Input.GetMouseButton(0)
			    && _isControlling
			    && Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit, 10000, _groundLayer))
			{
				var hitPoint = hit.point;
				var closestPoint = _collider.ClosestPoint(hitPoint);
				closestPoint.y = _moveComponent.Position.y;
				_moveComponent.SetDestination(closestPoint);
			}
		}
	}
}
