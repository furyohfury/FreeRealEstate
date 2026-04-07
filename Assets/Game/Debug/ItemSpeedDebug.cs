using UnityEngine;

namespace Game
{
    public sealed class ItemSpeedDebug : MonoBehaviour
    {
        public GameObject _go;
        public float _speed = 1f;
        public Transform _start;
        public Transform _end;
        public float _sqrDist = 0.1f;
        public bool _isActive = true;

        private void Update()
        {
            if (!_isActive)
                return;

            var dir = _end.position - _start.position;
            dir.Normalize();

            if ((_go.transform.position - _end.position).sqrMagnitude >= _sqrDist)
            {
                _go.transform.Translate(dir * (_speed * Time.deltaTime), Space.World);
            }
            else
            {
                _go.transform.position = _start.position;
            }
        }
    }
}
