using Game.GameMode.Session.WorldGeneration;
using GameWideSystems.GameStateManagement;

namespace Game.GameMode.Session.Controller
{
    public class SessionInitializationParameters : GameStateInitializationParameters
    {
        public WorldGenerationConfigs WorldGenerationConfigs;

        public SessionInitializationParameters(WorldGenerationConfigs worldGenerationConfigs)
        {
            WorldGenerationConfigs = worldGenerationConfigs;
        }
    }
}