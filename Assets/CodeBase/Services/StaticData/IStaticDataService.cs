using CodeBase.StaticData.Player;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData ForPlayer(PlayerTypeId typeId);
    }
}