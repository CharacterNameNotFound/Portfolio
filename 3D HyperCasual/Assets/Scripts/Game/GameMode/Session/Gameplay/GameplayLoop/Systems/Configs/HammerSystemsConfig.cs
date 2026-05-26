using UnityEngine;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems.Configs
{
    public class HammerSystemsConfig : ScriptableObject
    {
        [Header("Game field")]
        [field: SerializeField] public float HammerSize { get; private set; }
        [field: SerializeField] public float HammerRelaxator { get; private set; }
        [field: SerializeField] public Vector3 ActivationAmplitude { get; set; }
        
        [Header("UI")]
        [field: SerializeField] public float UIButtonRelaxator { get; private set; }
        [field: SerializeField] public Vector3 UIActivationAmplitude { get; set; }
        
        
    }
}