using CodeBase.Logic.Item;
using UnityEngine;

namespace CodeBase.Services.Factories.Level
{
    public interface ILevelFactory : IService
    {
        GameObject LoadGenerator(Vector3 at);
        Capsule LoadCapsule(Vector3 at);
    }
}