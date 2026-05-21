using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.Screen.StateMachineGeneric
{
    public abstract class TransferableSMUIScreen<TUISMContext, TScreenDependencies, TUISMScreeenState, TParams, TTransfer> 
        : SMUIScreen<TUISMContext, TScreenDependencies, TUISMScreeenState, TParams>
        where TUISMContext : IUISMContext 
        where TScreenDependencies : IUIScreenDependencies 
        where TUISMScreeenState: IUISMScreenState
        where TParams : IScreenParams
        where TTransfer : IScreenStateTransferScenario<TUISMContext,TScreenDependencies>
    {
        public virtual async UniTask SwitchView(TTransfer transferScenario,  CancellationToken cancellationToken)
        {
            await transferScenario.Transfer(cancellationToken);
        }
        
        
    }
}