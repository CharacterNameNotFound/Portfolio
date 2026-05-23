using System;

namespace GameWideSystems.LocalizationWrapper
{
    [Serializable]
    public class LocalizedLineKey
    {
        public string Key;
        public TranslationCategory Category;
        
        public LocalizedLineKey(string key, TranslationCategory category)
        {
            Key = key;
            Category = category;
        }
    }
}