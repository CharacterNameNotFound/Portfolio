using System.Collections.Generic;
using Game.GameMode.Session.Game.Data.Enteties;

namespace Game.GameMode.Session.Game.Data
{
    public class SessionRegistry
    {
        // "session"
        public SessionScenario SessionScenario;
        
        // player
        public PlayerCharacterComponent PlayerCharacterComponent;
        public PlayerCameraComponent PlayerCameraComponent;
        public PlayerStats PlayerStats = new();
        public GameField GameField;
        
        // environment
        public List<DecorationComponent> Decorations = new();
        public List<EnemyComponent> Enemies = new();
        public List<ExpGem> ExpGems = new();
    }
}