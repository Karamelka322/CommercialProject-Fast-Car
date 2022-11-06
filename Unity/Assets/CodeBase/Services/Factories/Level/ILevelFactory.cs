using System.Threading.Tasks;
using CodeBase.Logic.Item;
using CodeBase.Services.Random;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        Task<GameObject> LoadEnergyAsync(PointData spawnPoint);
        Task LoadGeneratorAsync(PointData spawnPoint);
        Task<GameObject> LoadResourcesGeneratorAsync();
        Task<GameObject> LoadResourcesEnergyAsync();
    }
}