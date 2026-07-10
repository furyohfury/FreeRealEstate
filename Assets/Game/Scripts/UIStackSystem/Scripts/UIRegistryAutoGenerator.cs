#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UIStackSystem.Editor
{
    /// <summary>
    /// Поддерживает UIRegistry в актуальном состоянии.
    ///
    /// Ответственность:
    /// - Создать UIRegistry, если его нет.
    /// - Найти все Page-префабы проекта.
    /// - Синхронизировать PagesPrefabs, сохранив существующие настройки.
    /// - Запустить кодогенерацию анимаций.
    /// </summary>
    [InitializeOnLoad]
    static class UIRegistryAutoGenerator
    {
        private const string ResourcesFolder = "Assets/Resources";
        private const string RegistryPath = ResourcesFolder + "/UIRegistry.asset";

        static UIRegistryAutoGenerator()
        {
            // Выполняем после завершения импорта/компиляции,
            // чтобы AssetDatabase была полностью готова.
            EditorApplication.delayCall += Generate;
        }

        private static void Generate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            UIRegistry registry = LoadOrCreateRegistry();

            SynchronizePages(registry);

            EditorUtility.SetDirty(registry);
            AssetDatabase.SaveAssets();

            // Следующим файлом реализуем кодогенерацию.
            UIAnimationCodeGenerator.Generate();
        }

        private static UIRegistry LoadOrCreateRegistry()
        {
            UIRegistry registry = AssetDatabase.LoadAssetAtPath<UIRegistry>(RegistryPath);

            if (registry != null)
                return registry;

            if (!AssetDatabase.IsValidFolder(ResourcesFolder))
                AssetDatabase.CreateFolder("Assets", "Resources");

            registry = ScriptableObject.CreateInstance<UIRegistry>();

            AssetDatabase.CreateAsset(registry, RegistryPath);
            AssetDatabase.SaveAssets();

            return registry;
        }

        private static void SynchronizePages(UIRegistry registry)
        {
            // Используем словарь для сохранения уже существующих настроек
            // (анимации, длительности и т.п.).
            Dictionary<Page, PagePrefabInfo> existing =
                registry.PagesPrefabs?.Where(x => x != null && x.PagePrefab != null).ToDictionary(x => x.PagePrefab)
                ?? new Dictionary<Page, PagePrefabInfo>();

            List<Page> pages = FindAllPages();

            PagePrefabInfo[] newArray = new PagePrefabInfo[pages.Count];

            for (int i = 0; i < pages.Count; i++)
            {
                Page page = pages[i];

                // Если страница уже была в Registry —
                // оставляем существующий объект со всеми настройками.
                if (existing.TryGetValue(page, out PagePrefabInfo info))
                {
                    newArray[i] = info;
                }
                else
                {
                    newArray[i] = new PagePrefabInfo
                                  {
                                      PagePrefab = page
                                  };
                }
            }

            registry.PagesPrefabs = newArray;
        }

        private static List<Page> FindAllPages()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            List<Page> result = new List<Page>();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab == null)
                    continue;

                // Используем GetComponent, потому что Page является базовым типом
                // и нас устраивают все наследники.
                Page page = prefab.GetComponent<Page>();

                if (page != null)
                    result.Add(page);
            }

            // Стабильная сортировка уменьшает количество лишних изменений asset
            // между генерациями.
            result.Sort((a, b) => string.CompareOrdinal(a.name, b.name));

            return result;
        }
    }
}

#endif
