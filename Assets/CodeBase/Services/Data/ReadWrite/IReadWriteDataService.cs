using UnityEngine;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface IReadWriteDataService : IService
    {
        void Register(GameObject gameObject);
        void Clenup();
        void InformSingleReaders();
        void StartStreaming();
        void StopStreaming();
        void InformSingleWriters();
        void InformSingleReaders(GameObject gameObject);
        void InformSingleWriters(GameObject gameObject);
    }
}