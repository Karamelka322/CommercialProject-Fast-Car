using CodeBase.Data.Perseistent;

namespace CodeBase.Services.Data.ReadWrite
{
    public interface IStreamingWriteData
    {
        void StreamingWriteData(PlayerPersistentData persistentData);
    }
}