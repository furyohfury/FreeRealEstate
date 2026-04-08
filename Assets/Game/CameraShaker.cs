using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class CameraShaker : Singleton<CameraShaker>
    {
        [Header("Настройки тряски")]
        [SerializeField]
        private Vector3 _strength = new Vector3(0.7f, 0.7f, 0); // Сила (амплитуда)
        [SerializeField]
        private int _vibrato = 10; // Частота колебаний
        [SerializeField]
        private float _randomness = 90f; // Разброс (в градусах)

        [Header("Дополнительно")]
        [SerializeField]
        private bool _fadeOut = true; // Плавное затухание в конце

        private Tween _shakeTween;
        private Vector3 _initialPos;

        protected override void Awake()
        {
            base.Awake();
            _initialPos = transform.position;
        }

        /// <summary>
        /// Запустить тряску камеры. Можно вызывать из других скриптов.
        /// </summary>
        /// <param name="duration"></param>
        public void Shake(float duration)
        {
            // Если тряска уже идет, завершаем её перед новой, чтобы не "наслаивать" смещение
            if (_shakeTween != null
                && _shakeTween.IsActive())
            {
                _shakeTween.Complete();
            }

            // DOShakePosition — стандартный метод DOTween для создания вибрации
            _shakeTween = DOTween.Sequence()
                                 .Append(transform.DOShakePosition(duration, _strength, _vibrato, _randomness, _fadeOut))
                                 .AppendCallback(RestorePosition);
        }

        private void RestorePosition()
        {
            transform.position = _initialPos;
        }

        // Пример вызова для теста (нажмите Пробел в игре)
#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Shake(0.5f);
            }
        }
#endif
    }
}
