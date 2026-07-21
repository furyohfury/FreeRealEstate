#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

[InitializeOnLoad]
static class UniTaskDependencyChecker
{
    private const string UniTaskPackageName = "com.cysharp.unitask";
    private const string UniTaskGitUrl = "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask";
    private const string UniTaskGitHubUrl = "https://github.com/Cysharp/UniTask";

    private const string SessionKey = "UniTaskDependencyPromptShown";

    static UniTaskDependencyChecker()
    {
        EditorApplication.delayCall += Check;
    }

    private static void Check()
    {
        if (SessionState.GetBool(SessionKey, false))
            return;

        if (IsUniTaskInstalled())
            return;

        SessionState.SetBool(SessionKey, true);
        UniTaskInstallWindow.ShowWindow();
    }

    internal static bool IsUniTaskInstalled()
    {
        var packages = PackageInfo.GetAllRegisteredPackages();

        if (packages.Any(package => package.name == UniTaskPackageName))
            return true;

        // Fallback for projects where UniTask was imported manually into Assets.
        return Type.GetType("Cysharp.Threading.Tasks.UniTask, UniTask") != null;
    }

    internal static void InstallUniTask()
    {
        UniTaskInstaller.Install(UniTaskGitUrl);
    }

    internal static void OpenUniTaskGitHub()
    {
        Application.OpenURL(UniTaskGitHubUrl);
    }
}

sealed class UniTaskInstallWindow : EditorWindow
{
    public static void ShowWindow()
    {
        var window = GetWindow<UniTaskInstallWindow>(true, "Missing Dependency");
        window.minSize = new Vector2(420, 170);
        window.maxSize = new Vector2(420, 170);
        window.ShowUtility();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(12);

        EditorGUILayout.LabelField("UniTask is required", EditorStyles.boldLabel);

        EditorGUILayout.Space(6);

        EditorGUILayout.LabelField("This module depends on UniTask, but UniTask is not installed in this project.", EditorStyles.wordWrappedLabel);

        EditorGUILayout.Space(12);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Install UniTask", GUILayout.Height(32)))
            {
                UniTaskDependencyChecker.InstallUniTask();
                Close();
            }

            if (GUILayout.Button("Open UniTask GitHub", GUILayout.Height(32)))
            {
                UniTaskDependencyChecker.OpenUniTaskGitHub();
                Close();
            }

            if (GUILayout.Button("Later", GUILayout.Height(32)))
            {
                Close();
            }
        }

        EditorGUILayout.Space(8);

        EditorGUILayout.HelpBox("Installation uses Unity Package Manager and adds UniTask from the official Git repository.", MessageType.Info);
    }
}

static class UniTaskInstaller
{
    private static AddRequest _request;

    public static void Install(string packageUrl)
    {
        if (_request != null && !_request.IsCompleted)
            return;

        _request = Client.Add(packageUrl);
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate()
    {
        if (_request == null || !_request.IsCompleted)
            return;

        EditorApplication.update -= OnEditorUpdate;

        if (_request.Status == StatusCode.Success)
        {
            Debug.Log($"UniTask installed: {_request.Result.packageId}");
        }
        else
        {
            Debug.LogError($"Failed to install UniTask: {_request.Error.message}");
        }

        _request = null;
    }
}
#endif
