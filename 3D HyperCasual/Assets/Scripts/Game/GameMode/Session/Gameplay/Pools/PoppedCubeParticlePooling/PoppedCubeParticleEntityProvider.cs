using Game.GameMode.Session.Gameplay.Entities;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.Session.Gameplay.Pools.PoppedCubeParticlePooling
{
    public class PoppedCubeParticleEntityProvider : AddressablePoolEntityProvider<PoppedParticlesComponent>
    {
        public PoppedCubeParticleEntityProvider(PoppedCubeParticleReferenceProvider assetReference) : base(assetReference.Asset)
        {
        }
    }
}