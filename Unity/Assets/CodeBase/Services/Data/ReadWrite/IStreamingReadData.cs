using CodeBase.Data.Perseistent;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface IStreamingReadData
    {
        void StreamingReadData(PlayerPersistentData persistentData);
    }
}