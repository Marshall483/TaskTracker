using System;

namespace Monads
{
    public class Either<TResult, TError>
    {
        public bool Succeeded { get; private set; }

        private TResult Result;
        private TError Error;

        public void FailWith(TError error)
        {
            Error = error;
            Succeeded = false;
        }

        public TResult GetResult =>
            Succeeded ? Result : throw new InvalidOperationException("Operation failed");

        public TError GetFail =>
            !Succeeded ? Error : throw new InvalidOperationException("Operation succeded");

        public static Either<TResult, TError> WithSuccess(TResult result)
        {
            var monad = new Either<TResult, TError>();

            monad.Succeeded = true;
            monad.Result = result;

            return monad;
        } 

        public static Either<TResult, TError> WithError(TError error)
        {
            var monad = new Either<TResult, TError>();

            monad.Succeeded = false;
            monad.Error = error;

            return monad;
        }
    }

    public struct Error
    {
        public string Message { get; set; }

        public Error(string errorMessage) =>
            Message = errorMessage;

        public static implicit operator Error(string errorMessage) =>
            new Error (errorMessage);
        
        public static explicit operator string(Error error) =>
            error.Message;
    }
}
