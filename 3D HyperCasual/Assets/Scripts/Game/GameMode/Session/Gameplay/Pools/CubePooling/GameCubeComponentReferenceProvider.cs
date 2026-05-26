using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Gameplay.Pools.CubePooling
{
    public class GameCubeComponentReferenceProvider : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Asset { get; private set; }
    }
}