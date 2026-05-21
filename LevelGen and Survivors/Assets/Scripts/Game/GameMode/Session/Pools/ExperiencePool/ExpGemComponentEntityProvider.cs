using Game.GameMode.Session.Game.Data.Entities;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Pools.ExperiencePool
{
    public class ExpGemComponentEntityProvider : AddressablePoolEntityProvider<ExpGemComponent>
    {
        public ExpGemComponentEntityProvider(ExpGemComponentReferenceProvider assetReference) : base(assetReference.Asset)
        {
        }
    }
}