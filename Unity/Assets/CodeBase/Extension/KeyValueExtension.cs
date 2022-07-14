using System;
using CodeBase.Data;

namespace CodeBase.Extension
{
    public static class KeyValueExtension
    {
        public static void SetValueToKey<TKey>(this KeyValue<TKey, bool>[] keyValue, in TKey key, in bool value)
        {
            for (int i = 0; i < keyValue.Length; i++)
            {
                if (keyValue[i].Key.GetType() == key.GetType())
                {
                    keyValue[i].Value = value;
                    return;
                }
            }
            
            throw new ArgumentOutOfRangeException();
        }

        public static KeyValue<TKey, TValue>[] New<TKey, TValue>(in string[] keys) where TKey : Enum
        {
            KeyValue<TKey, TValue>[] keyValues = new KeyValue<TKey, TValue>[keys.Length];
            
            for (int i = 0; i < keyValues.Length; i++) 
                keyValues[i].Key = (TKey) Enum.Parse(typeof(TKey), keys[i]);

            return keyValues;
        }
    }
}