using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Services.Data.ReadWrite
{
    [UsedImplicitly]
    public class ReadWriteDataService : IReadWriteDataService
    {
        private List<ISingleWriteData> _singleWriters { get; } = new List<ISingleWriteData>();
        private List<ISingleReadData> _singleReaders { get; } = new List<ISingleReadData>();

        private readonly IPersistentDataService _persistentDataService;

        public ReadWriteDataService(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        public void Register(GameObject gameObject)
        {
            foreach (ISingleReadData reader in gameObject.GetComponentsInChildren<ISingleReadData>()) 
                RegisterSingleReader(reader);

            foreach (ISingleWriteData writer in gameObject.GetComponentsInChildren<ISingleWriteData>()) 
                RegisterSingleWriter(writer);
        }
        
        public void InformSingleReaders()
        {
            foreach (ISingleReadData reader in _singleReaders) 
                reader.SingleReadData(_persistentDataService.PlayerData);
        }
        
        public void InformSingleReaders(GameObject gameObject)
        {
            foreach (ISingleReadData reader in gameObject.GetComponentsInChildren<ISingleReadData>())
                reader.SingleReadData(_persistentDataService.PlayerData);
        }
        
        public void InformSingleWriters()
        {
            foreach (ISingleWriteData writer in _singleWriters) 
                writer.SingleWriteData(_persistentDataService.PlayerData);
        }

        public void InformSingleWriters(GameObject gameObject)
        {
            foreach (ISingleWriteData writer in gameObject.GetComponentsInChildren<ISingleWriteData>())
                writer.SingleWriteData(_persistentDataService.PlayerData);
        }

        public void CleanUp()
        {
            _singleReaders.Clear();
            _singleWriters.Clear();
        }
        
        private void RegisterSingleReader(ISingleReadData reader) =>
            _singleReaders.Add(reader);
        
        private void RegisterSingleWriter(ISingleWriteData writer) =>
            _singleWriters.Add(writer);
    }
}