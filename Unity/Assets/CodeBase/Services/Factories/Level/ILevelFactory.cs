using System.Threading.Tasks;
using CodeBase.Logic.Item;
using CodeBase.Services.Random;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        Task<GameObject> LoadCapsule(PointData spawnPoint);
        Task LoadGenerator(PointData spawnPoint);
    }
}