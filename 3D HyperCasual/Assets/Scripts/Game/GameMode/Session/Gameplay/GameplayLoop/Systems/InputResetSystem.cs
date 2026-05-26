using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.Session.Gameplay.Inputs;

namespace Game.GameMode.Session.Gameplay.GameplayLoop.Systems
{
    public class InputResetSystem : ILoopedSystem
    {
        private InputBuffer _inputBuffer;

        public InputResetSystem(InputBuffer inputBuffer)
        {
            _inputBuffer = inputBuffer;
        }

        public UniTask Initialize(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask Update(float delta, SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            _inputBuffer.Clear();
            
            return UniTask.CompletedTask;
        }

        public UniTask CleanUp(SessionRegistry sessionRegistry, CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
        
    }
}