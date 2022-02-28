using System;
using System.Collections.Generic;
using CodeBase.Logic.Item;
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
        
        [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config")]
        public List<Vector3> CapsuleSpawnPoints;
        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config")]
        private void FillCapsuleSpawnPoints()
        {
            CapsuleSpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<CapsuleSpawnPoint>();

            CapsuleSpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                CapsuleSpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }
        
#endif
    }
}