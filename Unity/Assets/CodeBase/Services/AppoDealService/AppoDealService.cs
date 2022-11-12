using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using JetBrains.Annotations;

namespace CodeBase.Services.AppoDealService
{
    [UsedImplicitly]
    public class AppoDealService : IAppoDealService
    {
        private const string AppKey = "ca-app-pub-1385302330013925~8217619149";

        private const int AdTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | 
                                    AppodealAdType.RewardedVideo | AppodealAdType.Mrec;
        
        public void Initialize() => 
            Appodeal.Initialize(AppKey, AdTypes);
    }
}