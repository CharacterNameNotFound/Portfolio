using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Pools.ExperiencePool
{
    public class ExpGemComponentReferenceProvider : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Asset { get; private set; }
    }
}