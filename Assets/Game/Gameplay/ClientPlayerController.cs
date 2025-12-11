using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public sealed class ClientPlayerController : NetworkBehaviour
	{
		[SerializeField]
		private MoveComponent _moveComponent;
		[SerializeField]
		private LayerMask _groundLayer;
		private bool _isControlling;

		private BoxCollider _collider;

		[Inject]
		private void Construct(
			[Inject(Id = "ClientInputCollider")]
			BoxCollider collider)
		{
			_collider = collider;
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
				SendMoveRequestServerRpc(closestPoint);
			}
		}

		[Rpc(SendTo.Server)]
		private void SendMoveRequestServerRpc(Vector3 destination)
		{
			// Сервер сам решает, двигать ли игрока
			_moveComponent.SetDestination(destination);
		}
	}
}
