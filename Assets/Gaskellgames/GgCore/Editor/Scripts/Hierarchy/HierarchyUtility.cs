#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gaskellgames.EditorOnly
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    [InitializeOnLoad]
    public static class HierarchyUtility
    {
        #region Variables
        
        private static GaskellgamesHubSettings_SO settings;
        private static SerializedDictionary<Type, string> hierarchyIcons;
        private static SerializedDictionary<int, HierarchyData> hierarchyObjectCache;
        
        public static event Action onCacheHierarchyIcons;
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Constructor

        static HierarchyUtility()
        {
            GgGUI.onCacheGgGUIIcons -= Initialisation;
            GgGUI.onCacheGgGUIIcons += Initialisation;
        }
        
        private static void Initialisation()
        {
            // initialise references
            settings = EditorExtensions.GetAssetByType<GaskellgamesHubSettings_SO>();
            hierarchyIcons ??= new SerializedDictionary<Type, string>();
            onCacheHierarchyIcons?.Invoke();
            
            // handle initialise editor
            GgEditorCallbacks.OnSafeInitialize -= OnSafeInitialize;
            GgEditorCallbacks.OnSafeInitialize += OnSafeInitialize;
            
            // handle scene loads
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneOpened += OnSceneOpened;
            
            // handle unknown updates to scene
            GgEditorCallbacks.OnSceneUnknownUpdate -= SceneUnknownUpdate;
            GgEditorCallbacks.OnSceneUnknownUpdate += SceneUnknownUpdate;
            
            // handle gameObject created
            GgEditorCallbacks.OnGameObjectCreated -= OnGameObjectCreated;
            GgEditorCallbacks.OnGameObjectCreated += OnGameObjectCreated;
            
            // handle gameObject destroyed
            GgEditorCallbacks.OnGameObjectDestroyed -= OnGameObjectDestroyed;
            GgEditorCallbacks.OnGameObjectDestroyed += OnGameObjectDestroyed;
            
            // handle prefab opened
            GgEditorCallbacks.OnPrefabOpened -= OnPrefabOpened;
            GgEditorCallbacks.OnPrefabOpened += OnPrefabOpened;
            
            // handle prefab updated
            GgEditorCallbacks.OnPrefabInstanceUpdated -= OnPrefabInstanceUpdated;
            GgEditorCallbacks.OnPrefabInstanceUpdated += OnPrefabInstanceUpdated;
            
            // handle component updates
            GgEditorCallbacks.OnGameObjectStructureUpdated -= OnGameObjectStructureUpdated;
            GgEditorCallbacks.OnGameObjectStructureUpdated += OnGameObjectStructureUpdated;
            
            // handle parenting changes
            GgEditorCallbacks.OnGameObjectParentUpdated -= OnGameObjectParentUpdated;
            GgEditorCallbacks.OnGameObjectParentUpdated += OnGameObjectParentUpdated;
            
            // handle drawing icons
            EditorApplication.hierarchyWindowItemOnGUI -= DrawHierarchy;
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchy;
            
            // initialise cache
            hierarchyObjectCache = new SerializedDictionary<int, HierarchyData>();
            CacheAllHierarchyObjectsInAllOpenScenes();
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Callbacks [TODO]
        
        private static void OnSafeInitialize()
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // cache all objects in opened scene
            CacheAllHierarchyObjectsInAllOpenScenes();
        }
        
        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // if single scene opened, clear current cache
            if (mode == OpenSceneMode.Single)
            {
                hierarchyObjectCache.Clear();
            } 
            
            // cache all objects in opened scene
            CacheAllHierarchyObjectsInScene(scene);
        }

        private static void SceneUnknownUpdate(Scene scene)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();

            // TODO - cache all objects in updated scene? Will be expensive if run often!
            //CacheAllHierarchyObjectsInScene(scene);
        }

        private static void OnGameObjectCreated(GameObject gameObject)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // cache parent or self
            CacheParentOrSelfRecursive(gameObject.transform, gameObject.scene);
        }

        private static void OnGameObjectDestroyed(int instanceID, GameObject parent, Scene scene)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // re-cache parent
            if (parent)
            {
                int parentID = parent.GetInstanceID();
                if (hierarchyObjectCache.TryGetValue(parentID, out HierarchyData oldHierarchyData))
                {
                    CacheHierarchyObjectRecursive(parent.transform, oldHierarchyData.indentLevel, oldHierarchyData.parentIsFinalChild, oldHierarchyData.isFinalChild);
                    return;
                }
            }
            
            // failsafe: cache whole scene
            CacheAllHierarchyObjectsInScene(scene);
        }

        private static void OnPrefabOpened(GameObject rootGameObject, SceneData sceneData)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // cache all objects in opened prefab
            CacheHierarchyObjectRecursive(rootGameObject.transform, 0, null, false);
        }

        private static void OnPrefabInstanceUpdated(GameObject prefabRoot, Scene scene)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // cache prefab parent or self
            CacheParentOrSelfRecursive(prefabRoot.transform, scene);
        }

        private static void OnGameObjectStructureUpdated(GameObject gameObject)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();

            // if exists, update components
            if (hierarchyObjectCache.TryGetValue(gameObject.GetInstanceID(), out HierarchyData hierarchyData))
            {
                CacheHierarchyObject(gameObject, hierarchyData.indentLevel, hierarchyData.parentIsFinalChild, hierarchyData.isFinalChild);
            }
        }

        private static void OnGameObjectParentUpdated(GameObject gameObject, GameObject newParent, GameObject oldParent, Scene newScene, Scene oldScene)
        {
            // initialise dictionary if required
            hierarchyObjectCache ??= new SerializedDictionary<int, HierarchyData>();
            
            // handle new parent
            CacheParentOrSelfRecursive(gameObject.transform, newScene);
            
            // handle old parent
            if (!oldParent) { return; }
            int oldParentID = oldParent.GetInstanceID();
            if (hierarchyObjectCache.TryGetValue(oldParentID, out HierarchyData oldHierarchyData))
            {
                CacheHierarchyObjectRecursive(oldParent.transform, oldHierarchyData.indentLevel, oldHierarchyData.parentIsFinalChild, oldHierarchyData.isFinalChild);
                return;
            }
            
            // failsafe: cache whole scene
            CacheAllHierarchyObjectsInScene(oldScene);
        }

        private static void DrawHierarchy(int instanceID, Rect position)
        {
            if (hierarchyObjectCache.TryGetValue(instanceID, out HierarchyData hierarchyData))
            {
                DrawHierarchyBreadcrumbs(position, hierarchyData);
            }
            DrawHierarchyComponentIcons(instanceID, position);
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Private Functions

        /// <summary>
        /// Cache all gameObjects in a all open scenes
        /// </summary>
        private static void CacheAllHierarchyObjectsInAllOpenScenes()
        {
            // force initialise/clear dictionary
            hierarchyObjectCache = new SerializedDictionary<int, HierarchyData>();
            hierarchyObjectCache.Clear();
            
            // cache all gameObjects in all open scenes
            List<Scene> scenes = SceneExtensions.GetAllOpenScenes();
            foreach (Scene scene in scenes)
            {
                CacheAllHierarchyObjectsInScene(scene);
            }
        }
        
        /// <summary>
        /// Cache all gameObjects in a specific scene
        /// </summary>
        /// <param name="scene"></param>
        private static void CacheAllHierarchyObjectsInScene(Scene scene)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            int rootObjectsCount = rootObjects.Length;
            int count = 0;
            foreach (GameObject gameObject in rootObjects)
            {
                CacheHierarchyObjectRecursive(gameObject.transform, 0, null, count == rootObjectsCount);
                count++;
            }
        }

        /// <summary>
        /// Cache object and all it's child objects recursively. Uses the parent of the passed object, or self if root object.
        /// </summary>
        /// <param name="transform">The transform of the object, to check for a valid parent, before caching parent or self</param>
        /// <param name="scene">The holding scene of the </param>
        private static void CacheParentOrSelfRecursive(Transform transform, Scene scene)
        {
            // if root object, cache object and any child objects
            if (transform.root == transform)
            {
                CacheHierarchyObjectRecursive(transform, 0, null, false);
                return;
            }
            
            // if not root, cache parent object and all parent's child objects
            int parentID = transform.parent.gameObject.GetInstanceID();
            if (hierarchyObjectCache.TryGetValue(parentID, out HierarchyData hierarchyData))
            {
                CacheHierarchyObjectRecursive(transform.parent, hierarchyData.indentLevel, hierarchyData.parentIsFinalChild, hierarchyData.isFinalChild);
                return;
            }
            
            // failsafe: cache whole scene
            CacheAllHierarchyObjectsInScene(scene);
        }

        /// <summary>
        /// Cache object and all it's child objects recursively.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="indentLevel"></param>
        /// <param name="parentIsFinalChild"></param>
        /// <param name="isFinalChild"></param>
        private static void CacheHierarchyObjectRecursive(Transform transform, int indentLevel, List<bool> parentIsFinalChild, bool isFinalChild)
        {
            int childCount = transform.childCount;
            CacheHierarchyObject(transform.gameObject, indentLevel, parentIsFinalChild, isFinalChild);
            
            // recursive call for children
            int count = 0;
            List<bool> newParentIsFinalChild = new List<bool>();
            if (parentIsFinalChild != null) { newParentIsFinalChild.AddRange(parentIsFinalChild); }
            newParentIsFinalChild.Add(isFinalChild);
            foreach (Transform childTransform in transform)
            {
                count++;
                CacheHierarchyObjectRecursive(childTransform, indentLevel + 1, newParentIsFinalChild, count == childCount);
            }
        }
        
        /// <summary>
        /// Cache hierarchy info for a specific object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="indentLevel"></param>
        /// <param name="parentIsFinalChild"></param>
        /// <param name="isFinalChild"></param>
        private static void CacheHierarchyObject(GameObject gameObject, int indentLevel, List<bool> parentIsFinalChild, bool isFinalChild)
        {
            if (hierarchyIcons == null) { return; }
            int instanceID = gameObject.GetInstanceID();
            Component[] components = gameObject.GetComponents(typeof(Component));
            List<Type> validTypes = new List<Type>();
            foreach (Component component in components)
            {
                // cache only components for which there is an icon to draw
                if (component == null) { continue; }
                Type componentType = component.GetType();
                if (!hierarchyIcons.TryGetValue(componentType, out _)) { continue; }
                validTypes.TryAdd(componentType);
            }

            HierarchyData hierarchyData = new HierarchyData(indentLevel, 0 < gameObject.transform.childCount, parentIsFinalChild, isFinalChild, validTypes);
            hierarchyObjectCache.Remove(instanceID);
            hierarchyObjectCache.TryAdd(instanceID, hierarchyData);
        }
        
        /// <summary>
        /// Draw all component icons for a specific hierarchy object, at a given position
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="position"></param>
        private static void DrawHierarchyComponentIcons(int instanceID, Rect position)
        {
            if (settings == null) { return; }
            if (hierarchyIcons == null) { return; }
            if (hierarchyObjectCache == null) { return; }
            
            // draw if exists
            if (!hierarchyObjectCache.TryGetValue(instanceID, out HierarchyData hierarchyData)) { return; }
            int maxIcons;
            switch (settings.showHierarchyIcons)
            {
                default:
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.AllIcons:
                    maxIcons = hierarchyData.componentCount;
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.ThreeIcons:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 3);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.TwoIcons:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 2);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.OneIcon:
                    maxIcons = Mathf.Min(hierarchyData.componentCount, 1);
                    break;
                
                case GaskellgamesHubSettings_SO.HierarchyIconOptions.NoIcons:
                    return;
            }
            for (int i = 0; i < maxIcons; i++)
            {
                if (!hierarchyIcons.TryGetValue(hierarchyData.components[i], out string outValue)) { continue; }
                DrawHierarchyComponent(position, GgGUI.GetIcon(outValue), i);
            }
        }
        
        /// <summary>
        /// Draw a component icon at a given position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="icon"></param>
        /// <param name="indent"></param>
        private static void DrawHierarchyComponent(Rect position, Texture icon, int indent = 0)
        {
            // check for valid draw
            if (Event.current.type != EventType.Repaint) { return; }

            // draw icon
            if (icon == null) { return; }
            float pixels = 16;
            float offset = pixels * (indent + 1);
            EditorGUIUtility.SetIconSize(new Vector2(pixels, pixels));
            Rect iconPosition = new Rect(position.xMax - offset, position.yMin, position.width, position.height);
            GUIContent iconGUIContent = new GUIContent(icon);
            EditorGUI.LabelField(iconPosition, iconGUIContent);
        }

        /// <summary>
        /// Draw breadcrumbs for a specific hierarchy object, at a given position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="hierarchyData"></param>
        private static void DrawHierarchyBreadcrumbs(Rect position, HierarchyData hierarchyData)
        {
            // check for valid draw
            if (settings.showHierarchyBreadcrumbs == GaskellgamesHubSettings_SO.HierarchyBreadcrumbOptions.Never) { return; }
            if (Event.current.type != EventType.Repaint) { return; }
            
            // draw icon
            float pixels = 16;
            EditorGUIUtility.SetIconSize(new Vector2(pixels, pixels));
            for (int i = 0; i <= hierarchyData.indentLevel; i++)
            {
                string breadcrumbTexture;
                if (i <= 0)
                {
                    breadcrumbTexture = hierarchyData.isFinalChild
                        ? hierarchyData.hasChild ? "Icon_Breadcrumb_D" : "Icon_Breadcrumb_E"
                        : hierarchyData.hasChild ? "Icon_Breadcrumb_B" : "Icon_Breadcrumb_C";
                }
                else if (CanDrawIconBreadcrumbA(i, hierarchyData))
                {
                    breadcrumbTexture = "Icon_Breadcrumb_A";
                }
                else
                {
                    continue;
                }
                float offset = ((pixels * 0.5f) + GgGUI.standardSpacing) + ((pixels - GgGUI.standardSpacing) * (i + 1));
                Rect iconPosition = new Rect(position.xMin - offset, position.yMin, position.width, position.height);
                EditorGUI.LabelField(iconPosition, GgGUI.IconContent(breadcrumbTexture));
            }
        }

        /// <summary>
        /// Return whether the Icon_Breadcrumb_A should be drawn at a specified indent level for specific hierarchyData.
        /// (i.e. whether the parent of an object at a specified indent level is not final child object)
        /// </summary>
        /// <param name="indentLevel"></param>
        /// <returns></returns>
        private static bool CanDrawIconBreadcrumbA(int indentLevel, HierarchyData hierarchyData)
        {
            if (indentLevel == hierarchyData.indentLevel) { return true; }
            if (indentLevel < 0) { return true; }
            if (hierarchyData.parentIsFinalChild == null) { return true; }
            if (hierarchyData.parentIsFinalChild.Count - 1 < indentLevel) { return true; }
            
            return !hierarchyData.parentIsFinalChild[^indentLevel];
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------
        
        #region Public Functions
        
        /// <summary>
        /// Try to add custom icons to the HierarchyIcon_GgCore hierarchyIcons list. For best results, subscribe to
        /// the HierarchyIcon_GgCore.<see cref="onCacheHierarchyIcons"/> action using a script that implements <see cref="InitializeOnLoadAttribute"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static bool TryAddHierarchyIcon(Type type, string name, Texture icon)
        {
            if (icon == null) { return false; }
            hierarchyIcons ??= new SerializedDictionary<Type, string>();
            if (!hierarchyIcons.TryAdd(type, name)) { return false; }
            if (GgGUI.TryAddCustomIcon(name, icon)) { return true; }
            
            hierarchyIcons.Remove(type);
            return false;
        }

        #endregion
        
    } // class end
}

#endif