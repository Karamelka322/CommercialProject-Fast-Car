using System;
using System.Collections.Generic;
using CodeBase.Logic.Item;
using CodeBase.Services.AssetProvider;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
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
        
        [Space, Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), InfoBox("Empty", InfoMessageType.Error , "CheckCapsuleSpawnPoints")]
        public List<Vector3> CapsuleSpawnPoints;
        
        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f), PropertySpace(SpaceBefore = 10)]
        private void FillCapsuleSpawnPoints()
        {
            CapsuleSpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<CapsuleSpawnPoint>();

            CapsuleSpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                CapsuleSpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }
        
        [UsedImplicitly]
        private bool CheckCapsuleSpawnPoints() => 
            CapsuleSpawnPoints == null || CapsuleSpawnPoints.Count == 0;
        
        [ShowIf("CheckCapsuleSpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<CapsuleSpawnPoint>(AssetPath.CapsuleSpawnPointPath), Vector3.zero, Quaternion.identity);

        [UsedImplicitly]
        private int MaxQuantity() => 
            CapsuleSpawnPoints.Count;

        [UsedImplicitly]
        private int MinQuantity()
        {
            if (UsingCapsule)
            {
                return CapsuleSpawnPoints.Count >= 1 ? 1 : 0;
            }
            else
            {
                Quantity = 0;
                CapsuleSpawnPoints.Clear();
                return 0;
            }
        }

#endif
    }
}