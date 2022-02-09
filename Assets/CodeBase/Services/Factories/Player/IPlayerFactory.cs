using CodeBase.Data.Static.Player;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public interface IPlayerFactory : IService
    {
        GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at);
        GameObject Player { get; }
    }
}