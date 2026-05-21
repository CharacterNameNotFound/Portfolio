using GameWideSystems.LocalizationWrapper;
using UnityEngine;

namespace Game.Routines.ProfileOperations.ProfileCreation
{
    public class ProfileCreationErrorLocalizationKeysSOProvider : ScriptableObject, IProfileCreationErrorLocalizationKeysProvider
    {
        [field: SerializeField] public TranslationCategory LocalizationGroup { get; private set; }
        [field: SerializeField] public string EmptyProfileName { get; private set; }
        [field: SerializeField] public string ProfileNameAlreadyInUse { get; private set; }
        [field: SerializeField] public string InvalidProfileNameSymbols { get; private set; }
        [field: SerializeField] public string ProfileNameToLong { get; private set; }
    }
}