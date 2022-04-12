using System;

namespace CodeBase.Data
{
    [Serializable]
    public struct KeyValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}