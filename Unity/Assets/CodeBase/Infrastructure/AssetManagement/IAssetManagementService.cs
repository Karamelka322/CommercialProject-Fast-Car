using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Services.AssetProvider
{
    public interface IAssetManagementService : IService
    {
        void InitializeAsync();
        Task<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        T Load<T>(string assetPath) where T : Object;
        T[] LoadAll<T>(string assetPath) where T : Object;
        void CleanUp();
        Task<T> LoadAsync<T>(string id) where T : class;
    }
}