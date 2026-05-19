using System.Collections.Generic;

namespace Game.GameMode.Session.Game.Data
{
    public class SessionRegistry
    {
        public PlayerCharacterComponent PlayerCharacterComponent = new();
        public PlayerCameraComponent PlayerCameraComponent = new();
        public PlayerStats PlayerStats = new();
        public GameField GameField;
        
        public List<DecorationComponent> Decorations = new();
        public List<Enemy> Enemy = new();
        public List<ExpGem> ExpGems = new();
    }
}