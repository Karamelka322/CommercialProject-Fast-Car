using System;
using System.Collections.Generic;
using CodeBase.Logic.Enemy;
using CodeBase.Services.AssetProvider;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [UsedImplicitly]
        public bool UsingEnemy;

        [BoxGroup("Config")]
        public List<SpawnEnemyStaticData> List;
        
        [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error, "CheckEnemySpawnPoints")]
        public List<Vector3> EnemySpawnPoints;

        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f), PropertySpace(SpaceBefore = 10)]
        private void FillEnemySpawnPoints()
        {
            EnemySpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();
            
            EnemySpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                EnemySpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }
        
        [UsedImplicitly]
        private bool CheckEnemySpawnPoints() => 
            EnemySpawnPoints == null || EnemySpawnPoints.Count == 0;
        
        [ShowIf("CheckEnemySpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<EnemySpawnPoint>(AssetPath.EnemySpawnPointPath), Vector3.zero, Quaternion.identity);
#endif
    }
}