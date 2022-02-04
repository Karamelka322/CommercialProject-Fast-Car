using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class GeometryStaticData
    {
        public string SceneName;
        
        [Space]
        public List<Vector3> PlayerSpawnPoints;
    }
}