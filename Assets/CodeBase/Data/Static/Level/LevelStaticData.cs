using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        public LevelTypeId LevelType;

        [Space] 
        public bool Generator;
        
        [Space, Header("Generator"), Min(0)]
        public float PowerChangeSpeed;
        
        [Space]
        public GeometryStaticData Geometry;
        
        [Space] 
        public PeriodicityStaticData Periodicity;
    }

    [Serializable]
    public class PeriodicityStaticData
    {
        public List<SpawnEnemyStaticData> SpawnEnemyStaticData;
    }

    [Serializable]
    public struct MaxMinValue
    {
        public float Max;
        public float Min;
    }
}