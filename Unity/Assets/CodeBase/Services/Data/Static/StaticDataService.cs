using CodeBase.Data.Static;
using CodeBase.Data.Static.Enemy;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;
using CodeBase.Services.AssetProvider;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetMenagementService _assetMenagementService;
        
        private readonly PlayerStaticData[] _playerStaticDatas;
        private readonly EnemyStaticData[] _enemyStaticDatas;
        public LevelStaticData[] LevelStaticDatas { get; }

        public StaticDataService(IAssetMenagementService assetMenagementService)
        {
            _assetMenagementService = assetMenagementService;

            _playerStaticDatas = LoadAllAsset<PlayerStaticData>(AssetPath.PlayerStaticDataPath);
            LevelStaticDatas = LoadAllAsset<LevelStaticData>(AssetPath.LevelStaticDataPath);
            _enemyStaticDatas = LoadAllAsset<EnemyStaticData>(AssetPath.EnemyStaticDataPath);
        }

        public PlayerStaticData ForPlayer(PlayerTypeId typeId)
        {
            for (int i = 0; i < _playerStaticDatas.Length; i++)
            {
                if (_playerStaticDatas[i].Type == typeId)
                    return _playerStaticDatas[i];
            }

            return default;
        }

        public GameObject ForInput(InputTypeId typeId)
        {
            return typeId switch
            {
                InputTypeId.Joystick => LoadAsset<GameObject>(AssetPath.JoystickInputPath),
                InputTypeId.Buttons => LoadAsset<GameObject>(AssetPath.ButtonsInputPath),
                InputTypeId.Areas => LoadAsset<GameObject>(AssetPath.AreasInputPath),
                _ => default
            };
        }

        public LevelStaticData ForLevel(LevelTypeId typeId)
        {
            for (int i = 0; i < LevelStaticDatas.Length; i++)
            {
                if (LevelStaticDatas[i].Type == typeId)
                    return LevelStaticDatas[i];
            }

            return default;
        }

        public EnemyStaticData ForEnemy(EnemyTypeId enemyType, EnemyDifficultyTypeId difficultyType)
        {
            for (int i = 0; i < _enemyStaticDatas.Length; i++)
            {
                if (_enemyStaticDatas[i].EnemyType == enemyType && _enemyStaticDatas[i].DifficultyType == difficultyType)
                    return _enemyStaticDatas[i];
            }

            return default;
        }

        private T LoadAsset<T>(string address) where T : Object => 
            _assetMenagementService.Load<T>(address);
        
        private T[] LoadAllAsset<T>(string address) where T : Object => 
            _assetMenagementService.LoadAll<T>(address);
    }
}