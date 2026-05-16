using System;

namespace Utils.UtilityTypes.Result
{
    public class ProcedureResult
    {
        public const string DefaultErrorLine = "empty_error";
        
        protected string _error;
        protected bool _isSuccess;
        protected Exception _exception;

        public string Error => _error;
        public Exception Exception => _exception;

        public ProcedureResult(bool isSuccess, string error, Exception exception)
        {
            _exception = exception;
            _isSuccess = isSuccess;
            _error = error;
        }
        
        public ProcedureResult(bool isSuccess, string error)
        {
            _exception = new Exception(error);
            _isSuccess = isSuccess;
            _error = error;
        }

        public ProcedureResult(bool isSuccess)
        {
            _isSuccess = isSuccess;
            _error = DefaultErrorLine;
            _exception = new Exception(DefaultErrorLine);
        }

        public bool IsSuccess()
        {
            return _isSuccess;
        }
        
        public bool IsFailure()
        {
            return !_isSuccess;
        }
        
        public bool IsFailure(out string error)
        {
            error = _error;
            return !_isSuccess;
        }
        
    }
}