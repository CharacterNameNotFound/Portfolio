using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.Screen.StateMachineGeneric
{
    public abstract class GenericSMUIScreen<TUISMContext, TScreenDependencies, TUISMScreeenState, TParams> 
        : SMUIScreen<TUISMContext, TScreenDependencies, TUISMScreeenState, TParams>
        where TUISMContext : IUISMContext 
        where TScreenDependencies : IUIScreenDependencies 
        where TUISMScreeenState : IUISMGenericState<TUISMContext,TScreenDependencies>
        where TParams : IScreenParams
    {
        public virtual async UniTask SwitchView(TUISMScreeenState view, CancellationToken cancellationToken)
        {
            if (CurrentView is not null)
            {
                await CurrentView.Hide(cancellationToken);
            }

            CurrentView = view;
            
            await view.Show(cancellationToken);
        }
    }
}