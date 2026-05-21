using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.Splines.CatmullRom;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect
{
    public class FlyingParticleScriptedVisualEffect : ScriptedVisualEffectBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        

        public override async UniTask Play(ScriptedVisualEffectParams parameters, CancellationToken cancellationToken)
        {

            FlyingParticleParameters flyingParams = (FlyingParticleParameters) parameters;
            
            _spriteRenderer.color = flyingParams.Color;
            
            gameObject.SetActive(true);

            // General number of sections in spline is points - 1, and two of them is pre- and post-movement section
            int sectionCount = flyingParams.CatmullRomModel.Points.Length - 3;

            float secondsPerSection = flyingParams.Duration / sectionCount;

            try
            {
                for (float t = 0; t < 1 && !cancellationToken.IsCancellationRequested; t += Time.deltaTime / secondsPerSection)
                {
                    IteratePoint(flyingParams.CatmullRomModel, t);
                    await UniTask.NextFrame(cancellationToken: cancellationToken);
                }
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
            
            parameters.Manager.ReturnEffect(this);
        }

        public override void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }
        
        
        private void IteratePoint(CatmullRomModel catmullRomModel, float time)
        {
            Vector2 point = CatmullRomInterpolator.GetPoint(catmullRomModel, time);

            transform.position = point;
        }
        
    }
}