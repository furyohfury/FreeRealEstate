using Game.Scripts.Shooter;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Game.Scripts.ShooterS
{
    public class CameraComponent : NetworkBehaviour
    {
        [SerializeField]
        private Transform _cameraTarget;
        [SerializeField]
        private AimPointComponent _aimPointComponent;
        private CinemachineCamera _vcam;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _vcam = FindFirstObjectByType<CinemachineCamera>();
                _vcam.Target = new CameraTarget
                               {
                                   TrackingTarget = _cameraTarget
                               };
            }
        }

        private void LateUpdate()
        {
            if (IsOwner == false)
            {
                return;
            }

            Vector2 cameraRotationToTarget = GetCameraRotationToTarget(_aimPointComponent.GetAimPoint(), _vcam.transform);
            _cameraTarget.rotation = Quaternion.Euler(cameraRotationToTarget);
        }

        private Vector2 GetCameraRotationToTarget(Vector3 targetPoint, Transform currentCamera)
        {
            // 1. Получаем вектор направления от физического центра камеры к целевой точке
            Vector3 directionToTarget = targetPoint - currentCamera.transform.position;

            // 2. Переводим этот вектор в кватернион вращения (куда камера должна быть повернута)
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // 3. Извлекаем углы Эйлера (в градусах)
            Vector3 eulerAngles = lookRotation.eulerAngles;

            // 4. Нормализуем углы Unity, чтобы они были в удобном диапазоне от -180 до 180 градусов.
            // (Unity по умолчанию возвращает углы от 0 до 360, что может сломать логику Cinemachine)
            float pitch = eulerAngles.x;
            if (pitch > 180f)
                pitch -= 360f;

            float yaw = eulerAngles.y;
            if (yaw > 180f)
                yaw -= 360f;

            // X = Угол наклона по вертикали (вверх/вниз)
            // Y = Угол поворота по горизонтали (влево/вправо)
            return new Vector2(pitch, yaw);
        }
    }
}
