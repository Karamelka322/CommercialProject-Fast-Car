using CodeBase.Data;
using CodeBase.Data.Static;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Services.Input;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData ForPlayer(PlayerTypeId typeId);
        GameObject ForInput(InputTypeId typeId);
        LevelStaticData ForLevel(LevelTypeId levelDataType);
    }
}