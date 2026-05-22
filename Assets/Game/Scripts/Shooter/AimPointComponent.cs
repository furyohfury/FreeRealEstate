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

            Vector3 targetPoint;

            // Делаем рейкаст, чтобы узнать, обо что спотыкается взгляд игрока
            if (Physics.Raycast(ray, out RaycastHit hit, _maxAimDistance, _aimLayerMask))
            {
                // Если попали в объект (стену, врага), стрела полетит ровно в эту точку
                targetPoint = hit.point;
            }
            else
            {
                // Если впереди пустота (небо), берем точку на максимальном расстоянии луча
                targetPoint = ray.GetPoint(_maxAimDistance);
            }

            targetPoint.y = _shootingHeight;
            return targetPoint;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var startpos = new Vector3(transform.position.x, _shootingHeight, transform.position.z);
            Gizmos.DrawLine(startpos, startpos + transform.forward * _maxAimDistance);
        }
    }
}
