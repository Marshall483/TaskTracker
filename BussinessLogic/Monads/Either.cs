using System;

namespace Monads
{
    public class Either<TResult, TError>
    {
        public bool Succeeded { get; private set; }

        private TResult Result;
        private TError Error;

        public void SuccessWith(TResult result)
        {
            Result = result;
            Succeeded = true;
        }

        public void FailWith(TError error)
        {
            Error = error;
            Succeeded = false;
        }

        public TResult GetResult() =>
            Succeeded ? Result : throw new InvalidOperationException("Operation failed");

        public TError GetFail() =>
            !Succeeded ? Error : throw new InvalidOperationException("Operation succeded");
    }
}
