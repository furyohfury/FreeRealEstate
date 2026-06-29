using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIStackSystem
{
    [CreateAssetMenu(menuName = nameof(UIStackSystem) + "/" + nameof(UIRegistry), fileName = nameof(UIRegistry))]
    public sealed class UIRegistry : ScriptableObject
    {
        [SerializeField]
        private Page[] _pagesPrefabs;
        private readonly Dictionary<Type, Page> _dictionary = new Dictionary<Type, Page>();

        public void Initialize()
        {
            for (int i = 0; i < _pagesPrefabs.Length; i++)
            {
                Page page = _pagesPrefabs[i];
                Type presenterType = GetPresenterType(page);

                _dictionary[presenterType] = page;
            }
        }

        public Page<TPresenter> GetPagePrefab<TPresenter>() where TPresenter : IPresenter
        {
            if (_dictionary.TryGetValue(typeof(TPresenter), out Page page))
            {
                return (Page<TPresenter>)page;
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
    }
}