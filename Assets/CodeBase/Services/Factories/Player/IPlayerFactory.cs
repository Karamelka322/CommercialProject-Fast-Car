using CodeBase.Data.Static.Player;
using UnityEngine;

namespace CodeBase.Services.Factories.Player
{
    public interface IPlayerFactory : IService
    {
        GameObject Player { get; }
        GameObject PreviewPlayer { get; }
        GameObject CreatePlayer(PlayerTypeId typeId, Vector3 at);
        void CreatePreviewPlayer(PlayerTypeId typeId, Transform parent);
        void RebuildBasePreviewPlayerObject(PlayerTypeId playerTypeId);
    }
}