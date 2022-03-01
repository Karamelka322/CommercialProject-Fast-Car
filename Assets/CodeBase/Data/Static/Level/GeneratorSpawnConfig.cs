using System;
using System.Collections.Generic;
using CodeBase.Editor;
using CodeBase.Services.AssetProvider;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class GeneratorSpawnConfig
    {
        [UsedImplicitly]
        public bool UsingGenerator;
        
        [BoxGroup("Config"), MinValue(0), GUIColor(0.8f, 0.8f, 0)]
        public float PowerChangeSpeed;
        
        [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), PropertySpace(SpaceAfter = 10), InfoBox("Empty", InfoMessageType.Error, "CheckGeneratorSpawnPoints")]
        public List<Vector3> GeneratorSpawnPoints;
        
        
#if UNITY_EDITOR
        
        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f)]
        private void FillGeneratorSpawnPoints()
        {
            GeneratorSpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<GeneratorSpawnPoint>();

            GeneratorSpawnPoints.Clear();
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                GeneratorSpawnPoints.Add(generatorSpawnPoints[i].WorldPosition);
        }
        
        [UsedImplicitly]
        private bool CheckGeneratorSpawnPoints() => 
            GeneratorSpawnPoints == null || GeneratorSpawnPoints.Count == 0;
        
        [ShowIf("CheckGeneratorSpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<GeneratorSpawnPoint>(AssetPath.GeneratorSpawnPointPath), Vector3.zero, Quaternion.identity);
#endif
    }
}