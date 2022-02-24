using UnityEngine;

namespace CodeBase.Services.Data.ReaderWriter
{
    public interface IReadWriteDataService : IService
    {
        void Register(GameObject gameObject);
        void Clenup();
        void InformReaders();
    }
}