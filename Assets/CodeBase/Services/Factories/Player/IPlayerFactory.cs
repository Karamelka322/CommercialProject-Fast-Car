using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public interface IPlayerFactory : IService
    {
        GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at);
    }
}