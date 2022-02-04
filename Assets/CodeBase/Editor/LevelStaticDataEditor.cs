using CodeBase.Data.Static.Level;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelStaticData = target as LevelStaticData;
            
            if (GUILayout.Button("Collect"))
            {
                PlayerSpawnPoint[] playerSpawnPoints = FindObjectsOfType<PlayerSpawnPoint>();
                levelStaticData.Geometry.PlayerSpawnPoints.Clear();

                for (int i = 0; i < playerSpawnPoints.Length; i++)
                    levelStaticData.Geometry.PlayerSpawnPoints.Add(playerSpawnPoints[i].WorldPosition);

                levelStaticData.Geometry.SceneName = SceneManager.GetActiveScene().name;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}