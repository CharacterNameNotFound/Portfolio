using GameWideSystems.SessionManagement.Sessions;

namespace Game.Session
{
    public class OfflineSession : ISession
    {
        private readonly string _profileName;
        
        public string DisplayedId => _profileName;
        public string InternalId => _profileName;

        public OfflineSession(string profileName)
        {
            _profileName = profileName;
        }
    }
}