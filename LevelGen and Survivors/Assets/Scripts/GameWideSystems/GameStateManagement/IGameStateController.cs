using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.GameStateManagement
{
    public interface IGameStateController
    {
        /// <summary>
        /// Use for data loading
        /// </summary>
        public UniTask<bool> Initialize(GameStateInitializationParameters parameters, CancellationToken cancellationToken = default);
        /// <summary>
        /// Use for view related operations
        /// </summary>
        public UniTask Start(GameStateStartParameters parameters, CancellationToken cancellationToken = default);
        public UniTask Load(IGameStateSerializationData gameStateSerializationData, CancellationToken cancellationToken = default);
        public UniTask Unload(CancellationToken cancellationToken = default);
        public UniTask Close(CancellationToken cancellationToken = default);
        public UniTask<bool> TryGetSaveState(out IGameStateSerializationData gameStateSerializationData, 
            CancellationToken cancellationToken = default);
    }
}