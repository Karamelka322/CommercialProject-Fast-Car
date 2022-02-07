using CodeBase.Data.Perseistent;

namespace CodeBase.Services.Data.ReaderWriter
{
    public interface IReadData
    {
        void ReadData(PlayerPersistentData persistentData);
    }
}