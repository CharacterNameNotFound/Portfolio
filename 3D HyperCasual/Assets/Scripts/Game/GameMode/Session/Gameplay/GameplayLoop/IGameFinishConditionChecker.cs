namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public interface IGameFinishConditionChecker
    {
        public bool IsGameFinished(SessionRegistry registry);
    }
}