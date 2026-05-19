using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Systems
{
    public interface IGameEndDecider
    {
        public bool IsSessionFinished(SessionRegistry sessionRegistry);
    }
}