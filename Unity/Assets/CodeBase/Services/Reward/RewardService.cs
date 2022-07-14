using CodeBase.Data.Static.Player;
using CodeBase.Extension;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Services.Reward
{
    public class RewardService : IRewardService
    {
        private readonly IPersistentDataService _persistentDataService;
        
        private bool UsingRewardCar => _persistentDataService.PlayerData.SessionData.LevelData.CurrentLevelConfig.Reward.Car.UsingRewardCar;
        private PlayerTypeId RewardCar => _persistentDataService.PlayerData.SessionData.LevelData.CurrentLevelConfig.Reward.Car.Type;

        public RewardService(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        public void TakeRewardToCompletingLevel() => 
            TryTakeRewardCar();

        private void TryTakeRewardCar()
        {
            if (UsingRewardCar == false)
                return;
            
            _persistentDataService.PlayerData.ProgressData.Players.SetValueToKey(RewardCar, true);
        }
    }
}