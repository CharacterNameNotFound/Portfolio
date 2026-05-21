using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;

namespace Game.GameMode.Session.Game.Items.BuffItem
{
    public class DamageBuffItem : PassiveItem
    {
        public float[] DamageMods;
        
        public override async UniTask OnObtained(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            await base.OnObtained(sessionRegistry, cancellationToken);

            sessionRegistry.PlayerStats.DamageModifier *= DamageMods[CurrentLevel - 1];
        }

        public override async UniTask OnUpgrade(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            sessionRegistry.PlayerStats.DamageModifier /= DamageMods[CurrentLevel - 1];
            
            await base.OnUpgrade(sessionRegistry, cancellationToken);
            
            sessionRegistry.PlayerStats.DamageModifier *= DamageMods[CurrentLevel - 1];
        }
    }
}