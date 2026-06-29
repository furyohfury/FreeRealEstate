using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIStackSystem
{
    [CreateAssetMenu(menuName = nameof(UIStackSystem) + "/" + nameof(UIRegistry), fileName = nameof(UIRegistry))]
    public sealed class UIRegistry : ScriptableObject
    {
        [SerializeField]
        private PagePrefabInfo[] _pagesPrefabs;
        private readonly Dictionary<Type, PagePrefabInfo> _dictionary = new Dictionary<Type, PagePrefabInfo>();

        public void Initialize()
        {
            for (int i = 0; i < _pagesPrefabs.Length; i++)
            {
                PagePrefabInfo pagePrefabInfo = _pagesPrefabs[i];
                Page page = pagePrefabInfo.PagePrefab;
                Type presenterType = GetPresenterType(page);
                _dictionary[presenterType] = pagePrefabInfo;
            }
        }

        public Page<TPresenter> GetPagePrefab<TPresenter>() where TPresenter : IPresenter
        {
            if (_dictionary.TryGetValue(typeof(TPresenter), out PagePrefabInfo pagePrefabInfo))
            {
                return (Page<TPresenter>)pagePrefabInfo.PagePrefab;
            }

            throw new Exception($"The type {typeof(TPresenter).FullName} was not found.");
        }

        public UIPageAnimationMode GetDefaultOpenAnimationMode<TPresenter>() where TPresenter : IPresenter
        {
            if (_dictionary.TryGetValue(typeof(TPresenter), out PagePrefabInfo pagePrefabInfo))
            {
                return pagePrefabInfo.DefaultOpenAnimationMode;
            }

            throw new Exception($"The type {typeof(TPresenter).FullName} was not found.");
        }
        
        public UIPageAnimationMode GetDefaultCloseAnimationMode<TPresenter>() where TPresenter : IPresenter
        {
            if (_dictionary.TryGetValue(typeof(TPresenter), out PagePrefabInfo pagePrefabInfo))
            {
                return pagePrefabInfo.DefaultCloseAnimationMode;
            }

            throw new Exception($"The type {typeof(TPresenter).FullName} was not found.");
        }
        
        private static Type GetPresenterType(Page page)
        {
            Type type = page.GetType();

            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(Page<>))
                {
                    return type.GetGenericArguments()[0];
                }

                type = type.BaseType;
            }

            throw new Exception($"Page {page.name} does not inherit Page<TPresenter>");
        }
        
        [Serializable]
        private sealed class PagePrefabInfo
        {
            public Page PagePrefab;
            public UIPageAnimationMode DefaultOpenAnimationMode;
            public UIPageAnimationMode DefaultCloseAnimationMode;
        }
    }
}