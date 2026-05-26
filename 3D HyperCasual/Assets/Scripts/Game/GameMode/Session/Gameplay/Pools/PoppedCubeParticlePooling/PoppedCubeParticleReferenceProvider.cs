using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling
{
    public class PoppedCubeParticleReferenceProvider : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Asset { get; private set; }
    }
}