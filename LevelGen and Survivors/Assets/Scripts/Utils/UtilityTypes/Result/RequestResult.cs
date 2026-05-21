using System;
using UnityEngine.Assertions;

namespace Utils.UtilityTypes.Result
{
    public class RequestResult<T> : ProcedureResult
    {
        private T _result;

        public RequestResult(T result) : base(true)
        {
            _result = result;
        }

        public RequestResult(string error) : base(false, error)
        {
            _result = default;
        }

        public RequestResult(Exception exception) : base(false, DefaultErrorLine, exception)
        {
        }

        public RequestResult<T> SetValue(T value = default, string error = null)
        {
            _result = value;
            _error = error;

            return this;
        }

        public RequestResult<T> GetValue(out T value)
        {
            value = _result;
            return this;
        }

        public T GetValue()
        {
            return _result;
        }

        public RequestResult<T> SafeGetValue(out T value)
        {
            if (IsFailure())
            {
                throw new AssertionException("It is impossible to fetch result value for failed operation", "Use unsafe variant if you what to have result fetched in any circumstances");
            }
            
            value = _result;
            return this;
        }

        public T SafeGetValue()
        {
            if (IsFailure())
            {
                throw new AssertionException("It is impossible to fetch result value for failed operation", "Use unsafe variant if you what to have result fetched in any circumstances");
            }
            
            return _result;
        }
        
        public (T result, string error) Deconstruct()
        {
            return (_result, _error);
        }

        public string GetError()
        {
            return _error;
        }

    }
}