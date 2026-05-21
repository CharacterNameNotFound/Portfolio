using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Systems
{
    public class GameEndDecider : IGameEndDecider
    {
        public bool IsSessionFinished(SessionRegistry sessionRegistry)
        {
            return sessionRegistry.PlayerStats.CurrentHp <= 0;
        }
    }
}