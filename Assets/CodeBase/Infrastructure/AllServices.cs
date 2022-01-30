using CodeBase.Services;
using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class AllServices
    {
        public void RegisterSingle<TService>(IService service) where TService : class, IService => 
            Interpolation<TService>.Service = service as TService;

        public TService Single<TService>() where TService : class, IService => 
            Interpolation<TService>.Service;

        private static class Interpolation<TService> where TService : class, IService
        {
            public static TService Service;
        }
    }
}