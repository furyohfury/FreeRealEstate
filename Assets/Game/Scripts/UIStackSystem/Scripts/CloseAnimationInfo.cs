using System;
using UnityEngine;

namespace UIStackSystem
{
    [Serializable]
    public sealed class CloseAnimationInfo
    {
        [SerializeReference]
        public IPageCloseAnimation Animation = new PageCloseNoAnimation();
        public float Duration;
    }
}