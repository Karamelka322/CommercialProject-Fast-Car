using UnityEngine;

namespace CodeBase.Data.Static.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level", fileName = "Level", order = 51)]
    public class LevelStaticData : ScriptableObject
    {
        public LevelTypeId Type;
        
        [Space, Header("Generator"), Min(0)]
        public float PowerChangeSpeed;
        
        [Space]
        public GeometryStaticData Geometry;
    }
}