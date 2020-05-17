using System;
using System.Collections.Generic;

namespace Libraries.UtilityLib
{
    public class DictionaryEx<K, V> : Dictionary<K, V>   
    {
        public class Entry
        {
            public readonly KeyValuePair<K, V> Item;
            public readonly K Key;
            public readonly V Value;

            public Entry(K key, V value)
            {
                Key = key;
                Value = value;
                Item = new KeyValuePair<K, V>(Key, Value);
            }
        }

        public Entry[] Entries { get { return GetEntries(); } }

        protected Entry[] GetEntries()
        {
            List<Entry> entries = new List<Entry>();
            foreach (var key in Keys)
            {
                entries.Add(new Entry(key, this[key]));
            }
            return entries.ToArray();
        }

        public Entry[] GetEntriesForValue(V value)
        {
            List<Entry> entries = new List<Entry>();
            var pairs = this.GetItemsForValue<K, V>(value);
            foreach (var pair in pairs)
            {
                entries.Add(new Entry(pair.Key, pair.Value));
            }
            return entries.ToArray();
        }

        public Entry GetEntryForValue(V value)
        {
            Entry entry = null;
            var pairs = this.GetItemsForValue<K, V>(value);
            if (pairs.Length > 0)
            {
                entry = new Entry(pairs[0].Key, pairs[0].Value);
            }
            return entry;
        }
    }
}

