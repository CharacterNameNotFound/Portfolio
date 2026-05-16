using GameWideSystems.LocalizationWrapper;

namespace Game.Routines.ProfileOperations.ProfileCreation
{
    public interface IProfileCreationErrorLocalizationKeysProvider
    {
        public TranslationCategory LocalizationGroup { get; }
        public string EmptyProfileName { get; }
        public string ProfileNameAlreadyInUse { get; }
        public string InvalidProfileNameSymbols { get; }
        public string ProfileNameToLong { get; }
    }
}