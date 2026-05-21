using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.Result
{
    public static class ProcedureResultExtensions
    {
        public static UniTask<ProcedureResult> AsUniTask(this ProcedureResult procedureResult)
        {
            return UniTask.FromResult(procedureResult);
        }
    }
}