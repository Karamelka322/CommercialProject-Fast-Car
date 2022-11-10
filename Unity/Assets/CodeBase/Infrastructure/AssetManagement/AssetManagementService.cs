using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Services.AssetProvider
{
    [UsedImplicitly]
    public class AssetManagementService : IAssetManagementService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _сache = new Dictionary<string, AsyncOperationHandle>();

        public void InitializeAsync() => 
            Addressables.InitializeAsync();

        public async Task<T> LoadAsync<T>(AssetReference assetReference) where T : class => 
            await LoadAsync<T>(assetReference.AssetGUID);

        public async Task<T> LoadAsync<T>(string id) where T : class
        {
            if (_сache.TryGetValue(id, out AsyncOperationHandle completedHandler))
            {
                if(completedHandler.IsDone)
                {
                    return completedHandler.Result as T;
                }

                return await completedHandler.Task as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(id);
            _сache[id] = handle;
            return await handle.Task;
        }

        public T Load<T>(string assetPath) where T : Object => 
            Resources.Load<T>(assetPath);

        public T[] LoadAll<T>(string assetPath) where T : Object => 
            Resources.LoadAll<T>(assetPath);

        public void CleanUp()
        {
            foreach (AsyncOperationHandle handle in _сache.Values) 
                Addressables.Release(handle);
            
            _сache.Clear();
        }
    }
}