using CodeBase.Services.Input;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData ForPlayer(PlayerTypeId typeId);
        GameObject ForInput(InputTypeId typeId);
    }
}