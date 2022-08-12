using CodeBase.Data.Perseistent;
using CodeBase.Data.Static.Level;
using CodeBase.Services.AssetProvider;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Level.Generator
{
    public class GeneratorBasket : MonoBehaviour, ISingleReadData
    {
        [SerializeField] 
        private Transform _centerBasket;

        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void SingleReadData(PlayerPersistentData persistentData)
        {
            if (TryGetRewardCar(persistentData, out GameObject prefab)) 
                InstantiateReward(prefab);
        }

        private bool TryGetRewardCar(PlayerPersistentData persistentData, out GameObject prefab)
        {
            RewardConfig config = persistentData.SessionData.LevelData.CurrentLevelConfig.Reward;
            
            
            if (config.Car.UsingRewardCar)
            {
                prefab = _staticDataService.ForPlayer(config.Car.Type).Preview.gameObject;
                return true;
            }
            else
            {
                LevelTypeId currentLevel = persistentData.SessionData.LevelData.CurrentLevelConfig.Type;
                LevelStaticData[] staticDatas = _staticDataService.LevelStaticDatas;

                for (int i = 0; i < staticDatas.Length; i++)
                {
                    if (!IsUsingRewardCar(staticDatas[i], currentLevel))
                        continue;
                    
                    prefab = _staticDataService.ForPlayer(staticDatas[i].Reward.Car.Type).Preview.gameObject;
                    return true;
                }
            }
            
            prefab = default;
            return false;
        }

        private static bool IsUsingRewardCar(LevelStaticData staticData, LevelTypeId currentLevel) => 
            staticData.Type > currentLevel && staticData.Reward.Car.UsingRewardCar;

        private void InstantiateReward(GameObject reward) => 
            Instantiate(reward, _centerBasket);
    }
}