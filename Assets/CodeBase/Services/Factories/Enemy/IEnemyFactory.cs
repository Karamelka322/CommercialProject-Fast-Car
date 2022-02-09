using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public interface IEnemyFactory : IService
    {
        void CreateEnemy(Transform player, Vector3 at);
    }
}