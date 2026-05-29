using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class AimPointComponent : NetworkBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float _maxAimDistance = 100f;
        [SerializeField]
        private LayerMask _aimLayerMask;
        [SerializeField]
        private float _shootingHeight = 1f;
        private Camera _camera;

        public override void OnNetworkSpawn()
        {
            _camera = Camera.main;
        }

        public Vector3 GetAimPoint()
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = _camera.ScreenPointToRay(screenCenter);

            var targetPoint = Physics.Raycast(ray,
                out RaycastHit hit,
                _maxAimDistance,
                _aimLayerMask)
                ? hit.point
                : ray.GetPoint(_maxAimDistance);

            targetPoint.y = _shootingHeight;
            return targetPoint;
        }

#if UNITY_EDITOR
        [SerializeField]
        private float _gizmosRadius = 0.1f;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 aimPoint = GetAimPoint();
            Gizmos.DrawWireSphere(aimPoint, _gizmosRadius);
        }
#endif
    }
}
