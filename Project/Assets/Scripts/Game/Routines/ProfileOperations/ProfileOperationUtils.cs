using System.IO;
using System.Linq;

namespace Game.Routines.ProfileOperations
{
    public static class ProfileOperationUtils
    {
        public static bool IsBlacklistSymbolsContained(string profileName)
        {
            return Path.GetInvalidPathChars().Any(profileName.Contains) || profileName.Contains(".");
        }
    }
}