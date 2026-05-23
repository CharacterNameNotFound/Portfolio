using System.Collections.Generic;
using Game.GameMode.Session.Game.Data.Entities;
using Game.GameMode.Session.Game.Items;
using GameWideSystems.AudioManager;
using GameWideSystems.ScriptedVisualEffectManagement;

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

        public List<IItem> ObtainedItems = new();
        
        // environment
        public List<DecorationComponent> Decorations = new();
        public List<EnemyComponent> Enemies = new();
        public List<ExpGemComponent> ExpGems = new();
        
        // external systems
        public AudioManager AudioManager;
        public IScriptedVisualEffectManager ScriptedVisualEffectManager;
    }
}