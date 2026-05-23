using System.IO;
using GameWideSystems.SessionManagement.Sessions;
using UnityEngine;

namespace Game.Session
{
    public class GenericPathProvider
    {
        private const string SaveFolderSuffix = "Saves";
        private const string InProfileSavesPaths = "save_files";

        
        // saves
        private readonly string _saveSource;
        private readonly string _inventorySource;
        
        public GenericPathProvider()
        {
            _saveSource = Path.Combine(Application.persistentDataPath, SaveFolderSuffix);
        }

        public string SaveFilesPath()
        {
            return _saveSource;
        }
        
        public string ProfileSaveFilesPath(ISession session)
        {
            return ProfileSaveFilesPath(session.InternalId);
        }

        public string InProfileSavesPath(string profileName)
        {
            return Path.Combine(ProfileSaveFilesPath(profileName), InProfileSavesPaths);
        }
        
        public string ProfileSaveFilesPath(string profileName)
        {
            return Path.Combine(_saveSource, profileName);
        }

        public string LookUpTableArchiveLocation()
        {
            return Path.Combine(Application.streamingAssetsPath, "LockUpTables");
        }
        
        public string LooUpTableLocation()
        {
            return Path.Combine(Application.persistentDataPath, "LockUpTables");
        }
        
        
    }
}