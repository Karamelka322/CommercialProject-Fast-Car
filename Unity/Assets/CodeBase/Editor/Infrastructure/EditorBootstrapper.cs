using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [UsedImplicitly]
    public static class EditorBootstrapper
    {
        private const string InitialScenePath = "Assets/Scenes/Initial.unity";

        [InitializeOnLoadMethod]
        private static void Bootstrap() => 
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(InitialScenePath);
    }
}