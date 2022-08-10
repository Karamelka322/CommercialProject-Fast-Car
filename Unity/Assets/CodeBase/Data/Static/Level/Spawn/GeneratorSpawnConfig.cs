using System;
using System.Collections.Generic;
using CodeBase.Editor;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Random;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class GeneratorSpawnConfig
    {
        [UsedImplicitly]
        public bool UsingGenerator;
        
        [BoxGroup("Config"), MinValue(0), GUIColor(0.8f, 0.8f, 0), PropertyOrder(0)]
        public int StartValuePower;
        
        [BoxGroup("Config"), MinValue(0), GUIColor(0.8f, 0.8f, 0), PropertyOrder(-1)]
        public float PowerChangeSpeed;

        [Title("Asset", titleAlignment: TitleAlignments.Right), BoxGroup("Config"), Required("Empty"), PropertyOrder(0)] 
        public AssetReferenceGameObject PrefabReference;

        [Title("Spawn Points", titleAlignment: TitleAlignments.Right), ReadOnly, BoxGroup("Config"), GUIColor(1f, 1f, 0), PropertySpace(SpaceAfter = 10), InfoBox("Empty", InfoMessageType.Error, "CheckGeneratorSpawnPoints"), PropertyOrder(2)]
        public PointData[] GeneratorSpawnPoints;


#if UNITY_EDITOR

        [ShowIf("NotEmpty"), BoxGroup("Config"), InlineEditor(InlineEditorModes.LargePreview), ShowInInspector, ReadOnly, PropertyOrder(1)]
        private GameObject Prefab => PrefabReference.editorAsset;

        [UsedImplicitly]
        private bool NotEmpty() => 
            PrefabReference.editorAsset;

        [Button("Collect"), BoxGroup("Config"), GUIColor(0.5f, 0.7f, 1f), PropertyOrder(2)]
        private void FillGeneratorSpawnPoints()
        {
            GeneratorSpawnPoint[] generatorSpawnPoints = Object.FindObjectsOfType<GeneratorSpawnPoint>();

            GeneratorSpawnPoints = new PointData[generatorSpawnPoints.Length];
            
            for (int i = 0; i < generatorSpawnPoints.Length; i++)
                GeneratorSpawnPoints[i] = new PointData(generatorSpawnPoints[i].WorldPosition, generatorSpawnPoints[i].WorldRotation);
        }
        
        [UsedImplicitly]
        private bool CheckGeneratorSpawnPoints() => 
            GeneratorSpawnPoints == null || GeneratorSpawnPoints.Length == 0;
        
        [ShowIf("CheckGeneratorSpawnPoints"), Button("Generate Spawn Point"), GUIColor(0.5f, 0.7f, 1f)]
        private void GenerateSpawnPoint() => 
            Object.Instantiate(Resources.Load<GeneratorSpawnPoint>(AssetPath.GeneratorSpawnPointPath), Vector3.zero, Quaternion.identity);
#endif
    }
}