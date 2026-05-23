using Cysharp.Threading.Tasks;

namespace GameWideSystems.LocalizationWrapper
{
    public interface ILocalizationManager
    {
        public UniTask Initialize();
        
        public string GetLocalized(LocalizedLineKey key);
        public string GetLocalized(LocalizedLineKey key, params object[] replacements);
        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine);
        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine);

        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine, params object[] replacements);
        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine, params object[] replacements);
    }
}