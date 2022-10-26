using System.Threading.Tasks;
using CodeBase.Data.Static.Enemy;
using CodeBase.Services.Random;
using UnityEngine;

namespace CodeBase.Services.Factories.Enemy
{
    public interface IEnemyFactory : IService
    {
        Task LoadEnemyAsync(int id, PointData spawnPoint);
        Task LoadAllResourcesEnemyAsync();
    }
}