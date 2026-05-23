using System;
using Cysharp.Threading.Tasks;

namespace Utils.UtilityTypes.Result
{
    public static class ProcedureResultBuilder
    {
        public static ProcedureResult Success()
        {
            return new ProcedureResult(true);
        }
        
        public static ProcedureResult Failure()
        {
            return new ProcedureResult(false);
        }
        
        public static ProcedureResult Failure(string error)
        {
            return new ProcedureResult(false, error);
        }

        public static ProcedureResult Failure(Exception exception)
        {
            return new ProcedureResult(false, ProcedureResult.DefaultErrorLine, exception);
        }
    }
}