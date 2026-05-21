using System;

namespace Utils.UtilityTypes.Result
{
    public class RequestResultBuilder
    {
        public static RequestResult<T> DefaultError<T>()
        {
            return new RequestResult<T>(ProcedureResult.DefaultErrorLine);
        }
        
        public static RequestResult<T> Error<T>(string error)
        {
            return new RequestResult<T>(error);
        }

        public static RequestResult<T> Result<T>(T value)
        {
            return new RequestResult<T>(value);
        }

        public static RequestResult<T> Result<T>(ProcedureResult procedureResult, T result)
        {
            return procedureResult.IsSuccess() 
                ? new RequestResult<T>(result) 
                : new RequestResult<T>(procedureResult.Error);
        }

        public static RequestResult<T> Error<T>(Exception exception)
        {
            return new RequestResult<T>(exception);
        }
    }
}