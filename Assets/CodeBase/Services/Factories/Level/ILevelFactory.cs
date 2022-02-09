using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        void LoadGenerator(Vector3 at);
    }
}