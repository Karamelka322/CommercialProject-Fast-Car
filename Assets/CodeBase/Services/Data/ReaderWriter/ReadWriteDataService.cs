using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.Data.ReaderWriter
{
    public class ReadWriteDataService : IReadWriteDataService
    {
        private List<IWriteData> _writers { get; } = new List<IWriteData>();
        private List<IReadData> _readers { get; } = new List<IReadData>();

        private readonly IPersistentDataService _persistentDataService;

        public ReadWriteDataService(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        public void Register(GameObject gameObject)
        {
            foreach (IReadData reader in gameObject.GetComponentsInChildren<IReadData>()) 
                RegisterReader(reader);

            foreach (IWriteData writer in gameObject.GetComponentsInChildren<IWriteData>()) 
                RegisterWriter(writer);
        }

        public void InformReaders()
        {
            foreach (IReadData reader in _readers) 
                reader.ReadData(_persistentDataService.PlayerData);
        }

        public void Clenup()
        {
            _readers.Clear();
            _writers.Clear();
        }
        
        private void RegisterReader(IReadData readerData) =>
            _readers.Add(readerData);

        private void RegisterWriter(IWriteData writerData) =>
            _writers.Add(writerData);
    }
}