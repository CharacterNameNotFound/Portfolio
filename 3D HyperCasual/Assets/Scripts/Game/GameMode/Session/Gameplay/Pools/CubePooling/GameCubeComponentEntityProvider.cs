using Game.GameMode.Session.Gameplay.Entities;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Pools.CubePooling
{
    public class GameCubeComponentEntityProvider : AddressablePoolEntityProvider<GameCubeComponent>
    {
        public GameCubeComponentEntityProvider(GameCubeComponentReferenceProvider assetReference) : base(assetReference.Asset)
        {
        }
    }
}