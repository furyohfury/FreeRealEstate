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
        [SerializeField]
        private ParticleSystem _dragFallingParticlesPrefab;
        [SerializeField]
        private float _dragFallingParticlesScale = 0.5f;
        [SerializeField]
        private float _wrongColorAnimDuration = 0.5f;

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
            AudioManager.Instance.PlayDestroyItemSFX(position);
        }

        public void SpawnCollisionVFX(Vector3 position)
        {
            ParticleSystem vfx = SpawnVFX(_collisionVFXPrefab, position);
            vfx.transform.localScale = Vector3.one * _collisionVFXScale;
            SetDestroyOnEnd(vfx);
            AudioManager.Instance.PlayCollisionSFX(position);
        }

        public void SpawnRightColorItemConsumedVFX(Vector3 position)
        {
            AudioManager.Instance.PlayRightColorItemConsumedSFX(position);
        }

        public void SpawnWrongColorItemConsumedVFX(Vector3 position)
        {
            CameraProvider.Instance.CameraFacade.ShakeCamera(_wrongColorAnimDuration);
            CameraProvider.Instance.CameraFacade.LaunchChromaticAbberation(_wrongColorAnimDuration);
            AudioManager.Instance.PlayWrongColorItemConsumedSFX(position);
        }

        public GameObject SpawnDragFallingParticlesVFX(Transform parent)
        {
            ParticleSystem vfx = Instantiate(_dragFallingParticlesPrefab, parent);
            vfx.transform.localScale = Vector3.one * _dragFallingParticlesScale;
            AudioManager.Instance.AddShimmerSource(vfx.gameObject);

            return vfx.gameObject;
        }

        public void ClearAllVFX()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
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
