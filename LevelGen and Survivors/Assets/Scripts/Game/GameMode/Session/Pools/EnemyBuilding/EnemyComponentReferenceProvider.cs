using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.Session.Pools.EnemyBuilding
{
    public class EnemyComponentReferenceProvider : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject EnemyAsset { get; private set; }
    }
}