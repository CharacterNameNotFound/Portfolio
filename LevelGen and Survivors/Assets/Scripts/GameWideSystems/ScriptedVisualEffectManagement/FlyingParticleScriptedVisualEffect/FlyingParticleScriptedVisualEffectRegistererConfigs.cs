using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect
{
    public class FlyingParticleScriptedVisualEffectRegistererConfigs : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject FlyingParticlesSprite { get; private set; }
        [field: SerializeField] public int FlyingParticlesPooledCount { get; private set; }
    }
}