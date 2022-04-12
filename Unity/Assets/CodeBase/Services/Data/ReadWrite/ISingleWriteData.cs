using CodeBase.Data.Perseistent;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface ISingleWriteData
    {
        void SingleWriteData(PlayerPersistentData persistentData);
    }
}