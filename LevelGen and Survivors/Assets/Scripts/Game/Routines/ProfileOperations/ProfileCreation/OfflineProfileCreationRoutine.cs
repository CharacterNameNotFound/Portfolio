using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Session;
using GameWideSystems.SessionManagement.Sessions;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ProfileOperations.ProfileCreation
{
    public class OfflineProfileCreationRoutine : IProfileCreationRoutine
    {
        private readonly OfflineProfileCreationValidation _offlineProfileCreationValidation;
        private readonly GenericPathProvider _genericPathProvider;
        private readonly SessionHolder _sessionHolder;
        private readonly IProfileCreationConfigurationsProvider _creationConfigurationsProvider;

        public OfflineProfileCreationRoutine(
            OfflineProfileCreationValidation offlineProfileCreationValidation, 
            GenericPathProvider genericPathProvider, 
            SessionHolder sessionHolder, 
            IProfileCreationConfigurationsProvider creationConfigurationsProvider)
        {
            _offlineProfileCreationValidation = offlineProfileCreationValidation;
            _genericPathProvider = genericPathProvider;
            _sessionHolder = sessionHolder;
            _creationConfigurationsProvider = creationConfigurationsProvider;
        }

        public UniTask<ProcedureResult> TryCreateProfile(string profileName, CancellationToken cancellationToken)
        {
            if (!_offlineProfileCreationValidation.IsNameCorrect(profileName, out var error))
            {
                return ProcedureResultBuilder.Failure(error).AsUniTask();
            }

            Directory.CreateDirectory(_genericPathProvider.ProfileSaveFilesPath(profileName));
            Directory.CreateDirectory(_genericPathProvider.InProfileSavesPath(profileName));
            _sessionHolder.SetSession(new OfflineSession(profileName));

            return ProcedureResultBuilder.Success().AsUniTask();
        }




        
    }
}