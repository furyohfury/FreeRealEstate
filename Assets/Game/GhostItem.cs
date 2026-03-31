using DG.Tweening;
using UnityEngine;

namespace Game
{
    public sealed class GhostItem : MonoBehaviour
    {
        private MeshRenderer[] _renderers;
        private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");
        private static readonly int _baseTex = Shader.PropertyToID("_BaseTex");
        private static readonly int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");

        private void Awake()
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
        }

        public void SetColor(Color ghostItemColor)
        {
            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                _renderers[i].material.color = ghostItemColor;
            }
        }

        public async Awaitable DestroyWithDissolve(Material dissolveMaterial, float dissolveAnimDuration)
        {
            SwitchToDissolve(dissolveMaterial);

            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                MeshRenderer renderer = _renderers[i];
                Material material = renderer.material;
                DOTween.To(() => material.GetFloat(_dissolveAmount), val => material.SetFloat(_dissolveAmount, val), 1, dissolveAnimDuration);
            }

            await Awaitable.WaitForSecondsAsync(dissolveAnimDuration);

            Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void SwitchToDissolve(Material dissolveMaterial)
        {
            for (int i = 0, count = _renderers.Length; i < count; i++)
            {
                var renderer = _renderers[i];
                Texture currentTexture = renderer.material.GetTexture(_baseMap);
                Material dissolveMat = new Material(dissolveMaterial);

                if (currentTexture != null)
                {
                    dissolveMat.SetTexture(_baseTex, currentTexture);
                }

                renderer.material = dissolveMat;
            }
        }
    }
}
