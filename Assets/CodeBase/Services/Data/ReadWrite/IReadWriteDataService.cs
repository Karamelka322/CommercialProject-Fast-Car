using UnityEngine;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface IReadWriteDataService : IService
    {
        void Register(GameObject gameObject);
        void Clenup();
        void InformReaders();
        void StartStreaming();
        void StopStreaming();
    }
}