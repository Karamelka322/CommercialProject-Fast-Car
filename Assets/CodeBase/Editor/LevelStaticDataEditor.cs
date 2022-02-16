using CodeBase.Data.Static.Level;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Item;
using CodeBase.Logic.Player;
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
                FillPlayerSpawnPoints(levelStaticData);
                FillGeneratorSpawnPoints(levelStaticData);
                FillEnemySpawnPoints(levelStaticData);
                FillCapsuleSpawnPoints(levelStaticData);
                FillSceneName(levelStaticData);
            }
            
            EditorUtility.SetDirty(target);
        }

        private static void FillPlayerSpawnPoints(LevelStaticData levelStaticData)
        {
            PlayerSpawnPoint[] playerSpawnPoints = FindObjectsOfType<PlayerSpawnPoint>();

            levelStaticData.Geometry.PlayerSpawnPoints.Clear();

            for (int i = 0; i < playerSpawnPoints.Length; i++)
                levelStaticData.Geometry.PlayerSpawnPoints.Add(playerSpawnPoints[i].WorldPosition);
        }

        private static void FillCapsuleSpawnPoints(LevelStaticData levelStaticData)
        {
            CapsuleSpawnPoint[] capsuleSpawnPoints = FindObjectsOfType<CapsuleSpawnPoint>();

            levelStaticData.Geometry.CapsuleSpawnPoints.Clear();

            for (int i = 0; i < capsuleSpawnPoints.Length; i++)
                levelStaticData.Geometry.CapsuleSpawnPoints.Add(capsuleSpawnPoints[i].WorldPosition);
        }
        
        private static void FillGeneratorSpawnPoints(LevelStaticData levelStaticData)
        {
            GeneratorSpawnPoint[] generatorSpawnPoints = FindObjectsOfType<GeneratorSpawnPoint>();
            
            levelStaticData.Geometry.GeneratorSpawnPoints.Clear();

            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                levelStaticData.Geometry.GeneratorSpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }

        private static void FillEnemySpawnPoints(LevelStaticData levelStaticData)
        {
            EnemySpawnPoint[] playerSpawnPoints = FindObjectsOfType<EnemySpawnPoint>();

            levelStaticData.Geometry.EnemySpawnPoints.Clear();

            for (int i = 0; i < playerSpawnPoints.Length; i++)
                levelStaticData.Geometry.EnemySpawnPoints.Add(playerSpawnPoints[i].WorldPosition);
        }

        private static void FillSceneName(LevelStaticData levelStaticData) => 
            levelStaticData.Geometry.SceneName = SceneManager.GetActiveScene().name;
    }
}