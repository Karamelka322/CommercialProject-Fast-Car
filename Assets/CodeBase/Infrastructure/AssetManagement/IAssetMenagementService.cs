using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.AssetProvider
{
    public interface IAssetMenagementService : IService
    {
        void InitializeAsync();
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        T Load<T>(string assetPath) where T : Object;
        T[] LoadAll<T>(string assetPath) where T : Object;
        void ClaenUp();
    }
}