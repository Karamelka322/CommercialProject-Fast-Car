using CodeBase.Logic.Item;
using CodeBase.Services.Random;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        Capsule LoadCapsule(PointData spawnPoint);
        void LoadGenerator(AssetReference PrefabReference, PointData spawnPoint);
    }
}