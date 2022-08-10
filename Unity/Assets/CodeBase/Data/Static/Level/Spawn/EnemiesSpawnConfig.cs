using System;
using CodeBase.Logic.Enemy;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class EnemiesSpawnConfig
    {
        [UsedImplicitly]
        public bool UsingEnemy;

        [BoxGroup("Config")]
        public EnemySpawnConfig[] Enemies;
        
        [FormerlySerializedAs("EnemySpawnPoints")] [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error, "CheckEnemySpawnPoints")]
        public PointData[] SpawnPoints;

        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f), PropertySpace(SpaceBefore = 10)]
        private void CollectSpawnPoints()
        {
            EnemySpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();

            SpawnPoints = new PointData[generatorSpawnPoints.Length];
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                SpawnPoints[i] = new PointData(generatorSpawnPoints[i].WorldPosition, generatorSpawnPoints[i].WorldRotation);
        }
        
        [UsedImplicitly]
        private bool CheckEnemySpawnPoints() => 
            SpawnPoints == null || SpawnPoints.Length == 0;
        
        [ShowIf("CheckEnemySpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<EnemySpawnPoint>(AssetPath.EnemySpawnPointPath), Vector3.zero, Quaternion.identity);
#endif
    }
}