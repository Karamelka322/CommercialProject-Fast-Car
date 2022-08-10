using System;
using CodeBase.Logic.Player;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Object = UnityEngine.Object;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class PlayerSpawnConfig
    {
        [ReadOnly, GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error,"CheckPlayerSpawnPoints")]
        public PointData[] SpawnPoints;
        
#if UNITY_EDITOR
        
        [UsedImplicitly]
        private bool CheckPlayerSpawnPoints() => 
            SpawnPoints == null || SpawnPoints.Length == 0;

        [Button("Collect"), GUIColor(0.5f, 0.7f, 1f), PropertySpace(SpaceBefore = 10)]
        private void CollectSpawnPoints()
        {
            PlayerSpawnPoint[] spawnPoints = Object.FindObjectsOfType<PlayerSpawnPoint>();

            SpawnPoints = new PointData[spawnPoints.Length];
            
            for (int i = 0; i < spawnPoints.Length; i++)
                SpawnPoints[i] = new PointData(spawnPoints[i].WorldPosition, spawnPoints[i].WorldRotation);
        }
        
#endif
    }
}