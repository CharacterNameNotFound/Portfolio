using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ProfileOperations.ProfileCreation
{
    public interface IProfileCreationRoutine
    {
        public UniTask<ProcedureResult> TryCreateProfile(string profileName, CancellationToken cancellationToken);
    }
}