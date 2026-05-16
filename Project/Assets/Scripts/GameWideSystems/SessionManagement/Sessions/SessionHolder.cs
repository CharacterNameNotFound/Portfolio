namespace GameWideSystems.SessionManagement.Sessions
{
    public class SessionHolder
    {
        public ISession Session { get; private set; }

        public void SetSession(ISession session)
        {
            Session = session;
        }
        
    }
}