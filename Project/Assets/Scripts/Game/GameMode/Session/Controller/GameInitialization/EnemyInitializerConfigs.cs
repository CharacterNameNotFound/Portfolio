using UnityEngine;

namespace Game.GameMode.Session.Controller.GameInitialization
{
    public class EnemyInitializerConfigs : ScriptableObject
    {
        [field: SerializeField] public int PoolSize { get; private set; }
    }
}