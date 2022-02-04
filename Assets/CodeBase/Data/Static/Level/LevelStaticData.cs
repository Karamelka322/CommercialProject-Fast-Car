using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        public LevelTypeId Type;
        
        [Space]
        public GeometryStaticData Geometry;
    }
}