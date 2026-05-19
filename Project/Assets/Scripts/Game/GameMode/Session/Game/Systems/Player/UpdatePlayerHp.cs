using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Game.Data;
using UnityEngine;

namespace Game.GameMode.Session.Game.Systems.Player
{
    public class UpdatePlayerHp : ILoopedSystem
    {
        private static readonly int HpShaderRef = Shader.PropertyToID("_Hp");

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float deltaTime, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            float hp = sessionRegistry.PlayerStats.CurrentHp / sessionRegistry.PlayerStats.MaxHp;
            sessionRegistry.PlayerCharacterComponent.HpBar.material.SetFloat(HpShaderRef, hp);
            
            return UniTask.CompletedTask;
        }
        
    }
}