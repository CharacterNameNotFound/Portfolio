using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameWideSystems.UIManagement.Screen
{
    public abstract class UIScreenBase : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public CanvasGroup RootCanvasGroup { get; protected set; }
        public abstract ScreenType ScreenType { get; }
        public abstract ScreenHolderType ScreenHolderType { get; }
        public IUIScreenBuilder ScreenBuilder { get; private set; }
        public bool IsPreloaded { get; set; }

        protected UniTaskCompletionSource CompletionSource; // produces a token that corresponds to screen lifetime
        protected CancellationToken CancellationToken; // produces a token that corresponds to screen lifetime
        
        public void SetScreenBuilder(IUIScreenBuilder screenBuilder)
        {
            ScreenBuilder = screenBuilder;
        }
        
        // Called on screen being loaded for both immediate usage or one time only 
        public abstract UniTask Initialize(IUIScreenDependencies uiScreenDependencies, CancellationToken cancellationToken);

        // Called after request to show screen, for both open and open silently
        public virtual UniTask<UniTask> OnBeforeOpen(IScreenParams screenParams, CancellationToken cancellationToken)
        {
            CompletionSource = new UniTaskCompletionSource();
            CancellationToken = CompletionSource.Task.ToCancellationToken();
            
            return UniTask.FromResult(CompletionSource.Task);
        }
        
        // Used for intro animation
        public abstract UniTask OnOpen(CancellationToken cancellationToken);
        // Used to show screen without intro animation
        public abstract UniTask OnOpenSilently(CancellationToken cancellationToken);

        // Used for outro animation
        public abstract UniTask OnClose(CancellationToken cancellationToken);
        // Used to hide screen without outro animation
        public abstract UniTask OnCloseSilently(CancellationToken cancellationToken);
        
        public virtual void OnAfterClose()
        {
            CompletionSource?.TrySetResult();
            CompletionSource = null;
        }
        
        // Called before screen full closure
        public virtual void Dispose() { }
        
        public virtual UniTask<UniTask> OnOverlayRemoved(CancellationToken cancellationToken) { return UniTask.FromResult(CompletionSource.Task);}
        public virtual UniTask OnBecomingOverlaid(CancellationToken cancellationToken) { return UniTask.CompletedTask;}

    }
}