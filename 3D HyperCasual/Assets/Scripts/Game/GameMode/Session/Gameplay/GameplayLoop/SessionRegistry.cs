using System.Collections.Generic;
using Game.GameMode.Session.Gameplay.Entities;
using Game.GameMode.Session.UI;

namespace Game.GameMode.Session.Gameplay.GameplayLoop
{
    public class SessionRegistry
    {
        public List<GameCubeComponent> SpawningCubes;
        public List<GameCubeComponent> ActiveCubes;
        
        public GameFieldComponent GameFieldComponent;
        public SessionScreenController SessionScreen;

        public int Score;
        public int LivesMax;
        public int Lives;
    }
}