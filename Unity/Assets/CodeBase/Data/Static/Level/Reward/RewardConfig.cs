using System;
using Sirenix.OdinInspector;

namespace CodeBase.Data.Static.Level
{
    [Serializable]
    public class RewardConfig
    {
        [Toggle("UsingRewardCar")]
        public RewardCarData Car;
    }
}