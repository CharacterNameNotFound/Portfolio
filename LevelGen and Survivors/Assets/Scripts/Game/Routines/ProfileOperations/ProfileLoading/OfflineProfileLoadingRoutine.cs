using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Session;
using GameWideSystems.SessionManagement.Sessions;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ProfileOperations.ProfileLoading
{
    public class OfflineProfileLoadingRoutine : IProfileLoadingRoutine
    {
        private readonly SessionHolder _sessionHolder;

        public OfflineProfileLoadingRoutine(SessionHolder sessionHolder)
        {
            _sessionHolder = sessionHolder;
        }

        public UniTask<ProcedureResult> TryLoadProfile(string profileName, CancellationToken cancellationToken)
        {
            // It is possible to rename the folder, so we need some countermeasures
            if (ProfileOperationUtils.IsBlacklistSymbolsContained(profileName))
            {
                return ProcedureResultBuilder.Failure().AsUniTask();
            }
            
            _sessionHolder.SetSession(new OfflineSession(profileName));

            return ProcedureResultBuilder.Success().AsUniTask();
        }
    }
}