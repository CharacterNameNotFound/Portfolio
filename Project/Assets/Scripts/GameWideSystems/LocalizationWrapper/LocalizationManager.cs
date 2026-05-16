using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameWideSystems.LocalizationWrapper
{
    public class LocalizationManager : ILocalizationManager
    {

        private Logger.Logger _logger;
        
        private Dictionary<string, StringTable> _localTables;

        public LocalizationManager(Logger.Logger logger)
        {
            _logger = logger;
        }
        
        public async UniTask Initialize()
        {
            AsyncOperationHandle<IList<StringTable>> asyncOperationHandle = LocalizationSettings.StringDatabase.GetAllTables();
            await asyncOperationHandle.ToUniTask();

            IList<StringTable> result = asyncOperationHandle.Result;
            _localTables = result.ToDictionary(table => table.TableCollectionName);
        }

        public string GetLocalized(LocalizedLineKey key)
        {
            TryGetLocalized(key, out var line);
            return line;
        }

        public string GetLocalized(LocalizedLineKey key, params object[] replacements)
        {
            TryGetLocalized(key, out var line, replacements);
            return line;
        }

        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine)
        {
            return TryGetLocalized(key.Key, key.Category, out  localizedLine);
        }
        
        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine)
        {
            if (!GetTableEntry(key, category, out StringTableEntry entry))
            {
                localizedLine = key;
                return false;
            }

            localizedLine = entry.GetLocalizedString();
            return true;
        }
        
        public bool TryGetLocalized(LocalizedLineKey key, out string localizedLine, params object[] replacements)
        {
            return TryGetLocalized(key.Key, key.Category, out  localizedLine, replacements);
        }

        public bool TryGetLocalized(string key, TranslationCategory category, out string localizedLine, params object[] replacements)
        {
            if (!GetTableEntry(key, category, out StringTableEntry entry))
            {
                localizedLine = key;
                return false;
            }

            localizedLine = entry.GetLocalizedString(replacements);
            return true;
        }

        private bool GetTableEntry(string key, TranslationCategory category, out StringTableEntry tableEntry)
        {
            tableEntry = null;
            if (!_localTables.TryGetValue(category.ToString(), out StringTable table))
            {
                _logger.Log($"Table with name \"{category.ToString()}\" was not loaded");
                return false;
            }

            tableEntry = table.GetEntry(key);
            if (tableEntry is null)
            {
                _logger.Log($"Enty with name \"{key}\" was not present in \"{category.ToString()}\" table");
                return false;
            }

            return true;
        }
        
        
    }
}
