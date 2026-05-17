using UnityEngine;

namespace Game.Scripts.Shooter
{
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField]
        private float _cooldown;
        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = _cooldown;
                Shoot();
            }
        }

        public void Shoot()
        {
            GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.Euler(transform.forward));
        }
    }
}
