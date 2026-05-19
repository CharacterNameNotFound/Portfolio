using Game.GameMode.Session.Game.Data.Enteties;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Pools.EnemyBuilding
{
    public class EnemyComponentEntityProvider : AddressablePoolEntityProvider<EnemyComponent>
    {
        public EnemyComponentEntityProvider(EnemyComponentReferenceProvider assetReference) : base(assetReference.EnemyAsset)
        {
        }
    }
}