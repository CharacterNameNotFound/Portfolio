using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.Result
{
    public static class RequestResultExtensions
    {
        public static UniTask<RequestResult<T>> AsUniTask<T>(this RequestResult<T> item) 
        {
            return UniTask.FromResult(item);
        }

        public static RequestResult<T> AsRequestResult<T>(this T item)
        {
            return RequestResultBuilder.Result(item);
        }

        public static RequestResult<T> ToRequestResult<T>(this ProcedureResult procedureResult, T result)
        {
            return RequestResultBuilder.Result(procedureResult, result);
        }
        
        public static async UniTask<RequestResult<T>> ToAwaitedRequestResult<T>(this UniTask<ProcedureResult> procedureResult, T result)
        {
            ProcedureResult awaitResult = await procedureResult;
            return RequestResultBuilder.Result(awaitResult, result);
        }

        public static RequestResult<TDoll> AsConvertedFailureDoll<TBase, TDoll>(this RequestResult<TBase> item)
        {
            return !item.IsFailure(out string error) ? 
                RequestResultBuilder.Result<TDoll>(default) : 
                RequestResultBuilder.Error<TDoll>(error);
        }
        
        
    }
}