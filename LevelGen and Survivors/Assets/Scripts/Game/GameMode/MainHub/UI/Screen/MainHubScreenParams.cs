using Game.GameMode.Session.WorldGeneration;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.MainHub.UI.Screen
{
    public class MainHubScreenParams : IScreenParams
    {
        public WorldGenerationConfigs WorldGenerationConfigs;

        public MainHubScreenParams(WorldGenerationConfigs worldGenerationConfigs)
        {
            WorldGenerationConfigs = worldGenerationConfigs;
        }
    }
}