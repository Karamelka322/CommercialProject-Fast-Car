using CodeBase.logic.Player;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player", fileName = "Player", order = 51)]
    public class PlayerStaticData : ScriptableObject
    {
        public PlayerTypeId Type;
        public PlayerPrefab Prefab;
    }
}