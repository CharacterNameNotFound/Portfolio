using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Systems
{
    public interface ILoopedSystem
    {
        public void Update(float deltaTime, SessionRegistry sessionRegistry);
    }
}