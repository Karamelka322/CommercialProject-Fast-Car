using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyPrefab : MonoBehaviour, IReplayHandler
    {
        public void OnReplay() => 
            Destroy(gameObject);
    }
}