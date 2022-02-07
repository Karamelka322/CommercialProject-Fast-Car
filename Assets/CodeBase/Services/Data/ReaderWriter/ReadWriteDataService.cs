using System.Collections.Generic;
using CodeBase.Data.Perseistent;
using UnityEngine;

namespace CodeBase.Services.Data.ReaderWriter
{
    public class ReadWriteDataService : IReadWriteDataService
    {
        private List<IReadData> _readers { get; } = new List<IReadData>();
        private List<IWriteData> _writers { get; } = new List<IWriteData>();

        public void Register(GameObject gameObject)
        {
            foreach (IReadData reader in gameObject.GetComponentsInChildren<IReadData>()) 
                RegisterReader(reader);

            foreach (IWriteData writer in gameObject.GetComponentsInChildren<IWriteData>()) 
                RegisterWriter(writer);
        }

        public void InformReaders(PlayerPersistentData persistentData)
        {
            foreach (IReadData reader in _readers) 
                reader.ReadData(persistentData);
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