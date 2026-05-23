namespace GameWideSystems.SessionManagement.Sessions
{
    public interface ISession
    {
        public string DisplayedId { get; }
        public string InternalId { get; }
    }
}