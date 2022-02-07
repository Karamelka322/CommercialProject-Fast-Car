using CodeBase.Data.Perseistent;

namespace CodeBase.Services.Data.ReaderWriter
{
    public interface IWriteData
    {
        void WriteData(PlayerPersistentData persistentData);
    }
}