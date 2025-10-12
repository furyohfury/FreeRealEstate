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

		private void Update()
		{
			// Только локальный игрок обрабатывает ввод
			if (!IsOwner)
				return;

			if (Input.GetMouseButton(0) &&
			    Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit, 10000, _groundLayer))
			{
				var hitPoint = hit.point;
				hitPoint.y = _moveComponent.Position.y;
				_moveComponent.MoveTo(hitPoint);
			}
		}
	}
}