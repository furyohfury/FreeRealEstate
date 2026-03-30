using UnityEngine;

namespace Game
{
    public sealed class VFXManager : Singleton<VFXManager>
    {
        [SerializeField]
        private ParticleSystem _destroyItemVFX;
        [SerializeField]
        private float _destroyItemVFXScale = 0.5f;

        public void SpawnDestroyItemVFX(Vector3 position)
        {
            ParticleSystem vfx = SpawnVFX(_destroyItemVFX, position);
            vfx.transform.localScale = Vector3.one * _destroyItemVFXScale;
            SetDestroyOnEnd(vfx);
        }

        private ParticleSystem SpawnVFX(ParticleSystem ps, Vector3 position)
        {
            ParticleSystem vfx = Instantiate(ps, position, Quaternion.identity);
            return vfx;
        }

        private void SetDestroyOnEnd(ParticleSystem vfx)
        {
            var mainModule = vfx.main;
            mainModule.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}
