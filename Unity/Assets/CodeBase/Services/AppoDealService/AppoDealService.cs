using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using JetBrains.Annotations;

namespace CodeBase.Services.AppoDealService
{
    [UsedImplicitly]
    public class AppoDealService : IAppoDealService
    {
        private const string AppKey = "afb7b4133d689404b6ac310779663a32e1ae5aff9af5e9db";

        private const int AdTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | 
                                    AppodealAdType.RewardedVideo | AppodealAdType.Mrec;
        
        public void Initialize() => 
            Appodeal.Initialize(AppKey, AdTypes);
    }
}