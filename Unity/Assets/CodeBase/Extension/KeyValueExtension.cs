using CodeBase.Data;
using CodeBase.Data.Static.Level;
using CodeBase.Data.Static.Player;

namespace CodeBase.Extension
{
    public static class KeyValueExtension
    {
        public static bool TrySetValueToKey(this KeyValue<LevelTypeId, bool>[] keyValue, in LevelTypeId key, in bool value)
        {
            for (int i = 0; i < keyValue.Length; i++)
            {
                if (keyValue[i].Key == key)
                {
                    keyValue[i].Value = value;
                   return true;
                }
            }

            return false;
        }
        
        public static bool TrySetValueToKey(this KeyValue<PlayerTypeId, bool>[] keyValue, in PlayerTypeId key, in bool value)
        {
            for (int i = 0; i < keyValue.Length; i++)
            {
                if (keyValue[i].Key == key)
                {
                    keyValue[i].Value = value;
                    return true;
                }
            }

            return false;
        }
    }
}