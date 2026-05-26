using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs
{
    public class PlayerConfigs : ScriptableObject
    {
        [field: SerializeField] public int FailCount { get; private set; }
    }
}