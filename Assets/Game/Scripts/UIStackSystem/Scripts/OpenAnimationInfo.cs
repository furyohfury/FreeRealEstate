using System;
using UnityEngine;

namespace UIStackSystem
{
    [Serializable]
    public sealed class OpenAnimationInfo
    {
        [SerializeReference]
        public IPageOpenAnimation Animation = new PageOpenNoAnimation();
        public float Duration;
    }
}
