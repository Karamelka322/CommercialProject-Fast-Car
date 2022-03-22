using CodeBase.Data.Static;
using CodeBase.Data.Static.Enemy;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData ForPlayer(PlayerTypeId typeId);
        GameObject ForInput(InputTypeId typeId);
        EnemyStaticData ForEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType);
        LevelStaticData ForLevel(LevelTypeId typeId);
        LevelStaticData[] LevelStaticDatas { get; }
    }
}