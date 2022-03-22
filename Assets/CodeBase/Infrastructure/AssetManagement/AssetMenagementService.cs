using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Services.AssetProvider
{
    public class AssetMenagementService : IAssetMenagementService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _сache = new Dictionary<string, AsyncOperationHandle>();

        public void InitializeAsync() => 
            Addressables.InitializeAsync();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_сache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandler))
            {
                if(completedHandler.IsDone)
                {
                    return completedHandler.Result as T;
                }
                else
                {
                    return await completedHandler.Task as T;
                }
            }
            else
            {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
                _сache[assetReference.AssetGUID] = handle;
                return await handle.Task;
            }
        }

        public T Load<T>(string assetPath) where T : Object => 
            Resources.Load<T>(assetPath);

        public T[] LoadAll<T>(string assetPath) where T : Object => 
            Resources.LoadAll<T>(assetPath);

        public void ClaenUp()
        {
            foreach (AsyncOperationHandle handle in _сache.Values) 
                Addressables.Release(handle);
            
            _сache.Clear();
        }
    }
}