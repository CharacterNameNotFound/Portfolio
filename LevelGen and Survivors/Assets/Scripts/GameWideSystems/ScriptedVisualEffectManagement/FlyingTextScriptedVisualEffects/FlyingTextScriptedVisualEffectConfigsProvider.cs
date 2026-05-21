using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects
{
    public class FlyingTextScriptedVisualEffectConfigsProvider : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject FlyingTextScriptedVisualEffectInstance { get; set; }
        [field: SerializeField] public int PrebuildCount { get; set; } = 100;
        
    }
}