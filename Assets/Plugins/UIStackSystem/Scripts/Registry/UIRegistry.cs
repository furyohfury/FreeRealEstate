using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIStackSystem
{
    [CreateAssetMenu(menuName = nameof(UIStackSystem) + "/" + nameof(UIRegistry), fileName = nameof(UIRegistry))]
    public sealed class UIRegistry : ScriptableObject
    {
        public PagePrefabInfo[] PagesPrefabs;

        private readonly Dictionary<Type, PagePrefabInfo> _dictionary = new();

        public void Initialize()
        {
            _dictionary.Clear();

            for (int i = 0; i < PagesPrefabs.Length; i++)
            {
                PagePrefabInfo pageInfo = PagesPrefabs[i];

                if (pageInfo == null || pageInfo.PagePrefab == null)
                    continue;

                Type presenterType = GetPresenterType(pageInfo.PagePrefab);

                _dictionary[presenterType] = pageInfo;
            }
        }

        public Page<TPresenter> GetPagePrefab<TPresenter>()
            where TPresenter : IPresenter
        {
            return (Page<TPresenter>)GetPageInfo<TPresenter>().PagePrefab;
        }

        public OpenAnimationInfo GetDefaultOpenAnimationInfo<TPresenter>()
            where TPresenter : IPresenter
        {
            return GetPageInfo<TPresenter>().DefaultOpenAnimation;
        }

        public CloseAnimationInfo GetDefaultCloseAnimationInfo<TPresenter>()
            where TPresenter : IPresenter
        {
            return GetDefaultCloseAnimationInfo(typeof(TPresenter));
        }
        
        public CloseAnimationInfo GetDefaultCloseAnimationInfo(Type presenterType)
        {
            return GetPageInfo(presenterType).DefaultCloseAnimation;
        }

        private PagePrefabInfo GetPageInfo(Type presenterType)
        {
            if (_dictionary.TryGetValue(presenterType, out PagePrefabInfo info))
                return info;

            throw new Exception(
                $"The type {presenterType.FullName} was not found.");
        }

        private PagePrefabInfo GetPageInfo<TPresenter>()
            where TPresenter : IPresenter
        {
            return GetPageInfo(typeof(TPresenter));
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

            throw new Exception(
                $"Page {page.name} does not inherit Page<TPresenter>");
        }
    }
}