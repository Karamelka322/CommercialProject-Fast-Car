using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Data.Static.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player", fileName = "Player", order = 51)]
    public class PlayerStaticData : ScriptableObject
    {
        public PlayerTypeId Type;
        public PlayerPrefab Prefab;
        
        [Header("Stats"), Min(1)]
        public float Health;
    }
}