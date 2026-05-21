using System.Collections.Generic;
using System.Linq;

namespace Utils.UtilityTypes.AutoDictionaries
{
    public class AutoDictionary<TKey, TEntry> where TEntry : IAutoDictionaryEntry<TKey>
    {
        private readonly Dictionary<TKey, TEntry> _dictionary;
        
        public AutoDictionary(TEntry[] entries)
        {
            _dictionary = entries.ToDictionary(item => item.AutoDictionaryKey);
        }

        public bool TryGet(TKey key, out TEntry result)
        {
            return _dictionary.TryGetValue(key, out result);
        }
    }
}