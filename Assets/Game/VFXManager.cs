using UnityEngine;

namespace Game
{
    public sealed class VFXManager : Singleton<VFXManager>
    {
        [SerializeField]
        private ParticleSystem _destroyItemVFX;
        [SerializeField]
        private float _destroyItemVFXScale = 0.5f;
        [SerializeField]
        private ParticleSystem _collisionVFXPrefab;
        [SerializeField]
        private float _collisionVFXScale = 1f;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void SpawnDestroyItemVFX(Vector3 position)
        {
            ParticleSystem vfx = SpawnVFX(_destroyItemVFX, position);
            vfx.transform.localScale = Vector3.one * _destroyItemVFXScale;
            SetDestroyOnEnd(vfx);
            // TODO sfx
        }

        public void SpawnCollisionVFX(Vector3 position)
        {
            ParticleSystem vfx = SpawnVFX(_collisionVFXPrefab, position);
            vfx.transform.localScale = Vector3.one * _collisionVFXScale;
            SetDestroyOnEnd(vfx);
            // TODO sfx
        }

        public void SpawnRightColorItemConsumedVFX(Vector3 position)
        {
            // TODO sfx
        }

        public void SpawnWrongColorItemConsumedVFX(Vector3 position)
        {
            CameraProvider.Instance.CameraFacade.ShakeCamera();
            // TODO sfx
        }

        private ParticleSystem SpawnVFX(ParticleSystem ps, Vector3 position)
        {
            ParticleSystem vfx = Instantiate(ps, position, Quaternion.identity, transform);
            return vfx;
        }

        private void SetDestroyOnEnd(ParticleSystem vfx)
        {
            var mainModule = vfx.main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}
