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

			if (Input.GetMouseButton(0) &&
			    _isControlling &&
			    Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit, 10000, _groundLayer))
			{
				var hitPoint = hit.point;
				hitPoint.y = _moveComponent.Position.y;
				_moveComponent.SetDestination(hitPoint);
			}
		}
	}
}