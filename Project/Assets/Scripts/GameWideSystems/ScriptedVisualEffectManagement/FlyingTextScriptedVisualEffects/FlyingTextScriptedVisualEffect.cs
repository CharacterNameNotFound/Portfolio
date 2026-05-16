using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects
{
    public class FlyingTextScriptedVisualEffect : ScriptedVisualEffectBase
    {
        [SerializeField] private TMP_Text _renderer;
        
        public override async UniTask Play(ScriptedVisualEffectParams parameters, CancellationToken cancellationToken)
        {
            FlyingTextScriptedVisualEffectParams configs = (FlyingTextScriptedVisualEffectParams) parameters;
            
            
            //setting starting conditions
            transform.SetParent(configs.Parent);
            _renderer.color = Color.white;
            transform.position = configs.StartPosition;
            _renderer.text = configs.Text;
            _renderer.fontSize = configs.FontSize;
            gameObject.SetActive(true);
            
            float breakDuration = configs.Duration * configs.FadeAtPercentile;

            try
            {
                UniTask movementTask = transform.DOMove(configs.DestinationPosition, configs.Duration).Play().ToUniTask(cancellationToken: cancellationToken);

                await UniTask.WaitForSeconds(breakDuration, cancellationToken: cancellationToken);

                Color color = Color.white;
                color.a = configs.FadeValue;

                UniTask colorTask = DOVirtual.Color(
                    Color.white, 
                    color, 
                    configs.Duration - breakDuration,
                    doColor =>
                    {
                        _renderer.color = doColor;
                    }).Play().WithCancellation(cancellationToken: cancellationToken);

                await UniTask.WhenAll(movementTask, colorTask);
            }
            catch (OperationCanceledException e)
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
        
    }
}