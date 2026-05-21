using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Items.BuffItem
{
    public class RadiusProjectileBuffItem : PassiveItem
    {
        public float[] RadiusMods;
        
        public override async UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            await base.OnObtained(sessionRegistry, cancellationToken);

            sessionRegistry.PlayerStats.RadiusModifier *= RadiusMods[CurrentLevel - 1];
        }

        public override async UniTask OnUpgrade(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            sessionRegistry.PlayerStats.RadiusModifier /= RadiusMods[CurrentLevel - 1];
            
            await base.OnUpgrade(sessionRegistry, cancellationToken);
            
            sessionRegistry.PlayerStats.RadiusModifier *= RadiusMods[CurrentLevel - 1];
        }
    }
}