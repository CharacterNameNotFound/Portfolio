using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Session;
using GameWideSystems.LocalizationWrapper;

namespace Game.Routines.ProfileOperations.ProfileCreation
{
    public class OfflineProfileCreationValidation
    {
        private const int MaxNameLength = 14;
        
        private readonly IProfileCreationErrorLocalizationKeysProvider _profileCreationErrorLocalizationKeysProvider;
        private readonly ILocalizationManager _localizationManager;
        private readonly GenericPathProvider _genericPathProvider;
        

        public OfflineProfileCreationValidation(IProfileCreationErrorLocalizationKeysProvider profileCreationErrorLocalizationKeysProvider, ILocalizationManager localizationManager, GenericPathProvider genericPathProvider)
        {
            _profileCreationErrorLocalizationKeysProvider = profileCreationErrorLocalizationKeysProvider;
            _localizationManager = localizationManager;
            _genericPathProvider = genericPathProvider;
        }

        public bool IsNameCorrect(string name, out string error)
        {
            DirectoryInfo directory = new DirectoryInfo(_genericPathProvider.SaveFilesPath());
            List<string> profileList = directory.GetDirectories().Select(item => item.Name).ToList();


            if (string.IsNullOrWhiteSpace(name))
            {
                _localizationManager.TryGetLocalized(_profileCreationErrorLocalizationKeysProvider.EmptyProfileName,
                    _profileCreationErrorLocalizationKeysProvider.LocalizationGroup, out error);
                
                return false;
            }

            if (name.Length > MaxNameLength)
            {
                _localizationManager.TryGetLocalized(_profileCreationErrorLocalizationKeysProvider.ProfileNameToLong,
                    _profileCreationErrorLocalizationKeysProvider.LocalizationGroup, out error);

                return false;
            }

            if (profileList.Contains(name))
            {
                _localizationManager.TryGetLocalized(_profileCreationErrorLocalizationKeysProvider.ProfileNameAlreadyInUse,
                    _profileCreationErrorLocalizationKeysProvider.LocalizationGroup, out error);

                return false;
            }

            if (ProfileOperationUtils.IsBlacklistSymbolsContained(name))
            {
                _localizationManager.TryGetLocalized(_profileCreationErrorLocalizationKeysProvider.InvalidProfileNameSymbols,
                    _profileCreationErrorLocalizationKeysProvider.LocalizationGroup, out error);
                
                return false;
            }

            error = string.Empty;
            return true;
        }
        

    }
}