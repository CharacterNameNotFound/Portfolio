using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.Screen.StateMachineGeneric
{
    public interface IUISMGenericState <in TUISMContext, in TUIScreenDependencies> : IUISMScreenState where TUISMContext : IUISMContext where TUIScreenDependencies : IUIScreenDependencies
    {
        public UniTask Initialize(TUISMContext context, TUIScreenDependencies dependencies, CancellationToken cancellationToken);
        public UniTask Hide(CancellationToken cancellationToken);
        public UniTask Show(CancellationToken cancellationToken);
    }
}