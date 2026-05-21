using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.Screen.StateMachineGeneric
{
    public interface IScreenStateTransferScenario<TUISMContext, TScreenDependencies>
        where TUISMContext : IUISMContext 
        where TScreenDependencies : IUIScreenDependencies
    {
        public UniTask Transfer(CancellationToken cancellationToken);
    }
}