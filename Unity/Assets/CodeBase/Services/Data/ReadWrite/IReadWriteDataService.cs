using UnityEngine;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface IReadWriteDataService : IService
    {
        void Register(GameObject gameObject);
        void CleanUp();
        void InformSingleReaders();
        void InformSingleWriters();
        void InformSingleReaders(GameObject gameObject);
        void InformSingleWriters(GameObject gameObject);
    }
}