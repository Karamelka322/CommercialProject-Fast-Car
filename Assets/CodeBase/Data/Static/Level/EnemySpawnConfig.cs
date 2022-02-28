using System;
using System.Collections.Generic;
using CodeBase.Logic.Enemy;
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
        
        [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config")]
        public List<Vector3> EnemySpawnPoints;

#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config")]
        private void FillEnemySpawnPoints()
        {
            EnemySpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();
            
            EnemySpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                EnemySpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }
        
#endif
    }
}