using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Update;
using UnityEngine;

namespace CodeBase.Services.Data.ReadWrite
{
    public class ReadWriteDataService : IReadWriteDataService
    {
        private List<ISingleWriteData> _singleWriters { get; } = new List<ISingleWriteData>();
        private List<ISingleReadData> _singleReaders { get; } = new List<ISingleReadData>();
        private List<IStreamingWriteData> _streamingWriters { get; } = new List<IStreamingWriteData>();
        private List<IStreamingReadData> _streamingReaders { get; } = new List<IStreamingReadData>();

        private readonly IPersistentDataService _persistentDataService;
        private readonly IUpdateService _updateService;

        public ReadWriteDataService(IPersistentDataService persistentDataService, IUpdateService updateService)
        {
            _persistentDataService = persistentDataService;
            _updateService = updateService;
        }

        public void Register(GameObject gameObject)
        {
            foreach (ISingleReadData reader in gameObject.GetComponentsInChildren<ISingleReadData>()) 
                RegisterSingleReader(reader);

            foreach (ISingleWriteData writer in gameObject.GetComponentsInChildren<ISingleWriteData>()) 
                RegisterSingleWriter(writer);

            foreach (IStreamingReadData reader in gameObject.GetComponentsInChildren<IStreamingReadData>())
                RegisterStreamingReader(reader);
            
            foreach (IStreamingWriteData writer in gameObject.GetComponentsInChildren<IStreamingWriteData>())
                RegisterStreamingWriter(writer);
        }

        public void InformSingleReaders()
        {
            foreach (ISingleReadData reader in _singleReaders) 
                reader.SingleReadData(_persistentDataService.PlayerData);
        }
        
        public void InformSingleWriters()
        {
            foreach (ISingleWriteData writer in _singleWriters) 
                writer.SingleWriteData(_persistentDataService.PlayerData);
        }

        public void StartStreaming() => 
            _updateService.OnUpdate += StreammingData;

        public void StopStreaming() => 
            _updateService.OnUpdate -= StreammingData;

        private void StreammingData()
        {
            for (int i = 0; i < _streamingWriters.Count; i++)
                _streamingWriters[i].StreamingWriteData(_persistentDataService.PlayerData);
            
            for (int i = 0; i < _streamingReaders.Count; i++)
                _streamingReaders[i].StreamingReadData(_persistentDataService.PlayerData);
        }

        public void Clenup()
        {
            _singleReaders.Clear();
            _singleWriters.Clear();
            _streamingReaders.Clear();
            _streamingWriters.Clear();
        }
        
        private void RegisterSingleReader(ISingleReadData reader) =>
            _singleReaders.Add(reader);
        
        private void RegisterStreamingReader(IStreamingReadData reader) =>
            _streamingReaders.Add(reader);

        private void RegisterSingleWriter(ISingleWriteData writer) =>
            _singleWriters.Add(writer);
        
        private void RegisterStreamingWriter(IStreamingWriteData writer) =>
            _streamingWriters.Add(writer);
    }
}