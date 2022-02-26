using CodeBase.Logic.Enemy;
using UnityEngine;

namespace CodeBase.Data.Static.Enemy
{
    [CreateAssetMenu(menuName = "Static Data/Enemy", fileName = "Enemy", order = 51)]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyType;
        public EnemyDifficultyTypeId DifficultyType;

        [Space] 
        public EnemyPrefab Prefab;
    }
}