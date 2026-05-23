using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using UnityEngine;

namespace GameWideSystems.UIManagement.Screen
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIScreen<TParams, TDependencies> : UIScreenBase where TParams : IScreenParams where TDependencies : IUIScreenDependencies
    {
        public TParams Params { get; protected set; }
        public TDependencies Dependencies { get; protected set; }

        public override UniTask Initialize(IUIScreenDependencies uiScreenDependencies, CancellationToken cancellationToken)
        {
            Dependencies = (TDependencies)uiScreenDependencies;
            return UniTask.CompletedTask;
        }

        public override UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            Params = (TParams)screenParams;
            return base.OnBeforeOpen(screenParams, cancellationToken);
        }

        public override UniTask OnOpen(CancellationToken cancellationToken)
        {
            return GenericUIEntranceRoutines.ShowInstantly(RootCanvasGroup.gameObject, cancellationToken);
        }
        
        public override UniTask OnOpenSilently(CancellationToken cancellationToken)
        {
            return GenericUIEntranceRoutines.ShowInstantly(RootCanvasGroup.gameObject, cancellationToken);
        }
        
        public override async UniTask OnClose(CancellationToken cancellationToken)
        {
            await GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }

        public override UniTask OnCloseSilently(CancellationToken cancellationToken)
        {
            return GenericUIExitRoutines.HideInstantly(RootCanvasGroup.gameObject, cancellationToken);
        }
        
    }
}