using System;
using CodeBase.Logic.Item;
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
    public class CapsuleSpawnConfig
    {
        [UsedImplicitly]
        public bool UsingCapsule;

        [BoxGroup("Config"), MinValue("MinQuantity"), MaxValue("MaxQuantity"), GUIColor(0.8f, 0.8f, 0)]
        public int Quantity;
        
        [FormerlySerializedAs("CapsuleSpawnPoints")] [Space, Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error , "CheckCapsuleSpawnPoints")]
        public PointData[] SpawnPoints;
        
        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f), PropertySpace(SpaceBefore = 10)]
        private void CollectSpawnPoints()
        {
            CapsuleSpawnPoint[] spawnPoints = Object.FindObjectsOfType<CapsuleSpawnPoint>();
            SpawnPoints = new PointData[spawnPoints.Length];

            for (int i = 0; i < SpawnPoints.Length; i++) 
                SpawnPoints[i] = new PointData(spawnPoints[i].WorldPosition, spawnPoints[i].WorldRotation);
        }

        [UsedImplicitly]
        private bool CheckCapsuleSpawnPoints() => 
            SpawnPoints == null || SpawnPoints.Length == 0;
        
        [ShowIf("CheckCapsuleSpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<CapsuleSpawnPoint>(AssetPath.CapsuleSpawnPointPath), Vector3.zero, Quaternion.identity);

        [UsedImplicitly]
        private int MaxQuantity() => 
            SpawnPoints.Length;

        [UsedImplicitly]
        private int MinQuantity()
        {
            if (UsingCapsule)
            {
                return SpawnPoints.Length >= 1 ? 1 : 0;
            }
            else
            {
                Quantity = 0;
                SpawnPoints = Array.Empty<PointData>();
                return 0;
            }
        }

#endif
    }
}