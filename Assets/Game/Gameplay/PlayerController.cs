using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
	public sealed class PlayerController : NetworkBehaviour
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

			if (Input.GetMouseButton(0))
			{
				if (Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit, 10000, _groundLayer))
				{
					var hitPoint = hit.point;
					hitPoint.y = _moveComponent.Position.y;

					// Если это сервер (хост), выполняем сразу
					if (IsServer)
					{
						_moveComponent.MoveTo(hitPoint);
					}
					// Если клиент — шлём команду серверу
					else
					{
						SendMoveRequestServerRpc(hitPoint);
					}
				}
			}
		}

		[Rpc(SendTo.Server)]
		private void SendMoveRequestServerRpc(Vector3 destination)
		{
			// Сервер сам решает, двигать ли игрока
			_moveComponent.MoveTo(destination);
		}
	}
}