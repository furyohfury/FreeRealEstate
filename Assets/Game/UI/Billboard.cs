using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game
{
	public sealed class Billboard : MonoBehaviour
	{
		[ShowInInspector] [ReadOnly]
		private Camera _targetCamera;

		[Inject]
		private void Construct(Camera camera)
		{
			_targetCamera = camera;
		}

		private void Start()
		{
			if (_targetCamera == null)
			{
				_targetCamera = Camera.main; // используем основную камеру, если не указана явно
			}
		}

		private void LateUpdate()
		{
			Vector3 direction = transform.position - _targetCamera.transform.position;
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}
}