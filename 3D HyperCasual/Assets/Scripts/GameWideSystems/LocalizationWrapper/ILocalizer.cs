using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace GameWideSystems.LocalizationWrapper
{
    public interface ILocalizer
    {
        public UniTask<ProcedureResult> Localize(ILocalizationManager localizationManager, CancellationToken cancellationToken);
    }
}