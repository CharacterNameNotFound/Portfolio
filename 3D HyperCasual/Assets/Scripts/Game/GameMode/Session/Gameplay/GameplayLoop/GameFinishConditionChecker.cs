namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public class GameFinishConditionChecker : IGameFinishConditionChecker
    {
        public bool IsGameFinished(SessionRegistry registry)
        {
            return registry.Lives <= 0;
        }
    }
}