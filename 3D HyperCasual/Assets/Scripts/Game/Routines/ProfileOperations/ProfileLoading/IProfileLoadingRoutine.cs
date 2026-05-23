using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ProfileOperations.ProfileLoading
{
    public interface IProfileLoadingRoutine
    {
        public UniTask<ProcedureResult> TryLoadProfile(string profileName, CancellationToken cancellationToken);
    }
}