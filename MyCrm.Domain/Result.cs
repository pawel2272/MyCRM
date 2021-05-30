using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace MyCrm.Domain
{
    public class Result
    {

        internal Result(bool isSuccess, string message, IEnumerable<Error> errors)
        {
            IsSuccess = isSuccess;
            IsFailure = !isSuccess;
            Message = message;
            Errors = errors;
        }

        public string Message { get; }

        public bool IsFailure { get; }

        public bool IsSuccess { get; }

        public IEnumerable<Error> Errors { get; }

        public static Result Fail(string message)
        {
            return new Result(false, message, Enumerable.Empty<Error>());
        }

        public static Result Fail(ValidationResult validationResult)
        {
            return new Result(
                false,
                string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)),
                validationResult.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage))
            );
        }

        public static Result Ok()
        {
            return new Result(true, "", Enumerable.Empty<Error>());
        }

        public class Error
        {
            public Error(string propertyName, string message)
            {
                PropertyName = propertyName;
                Message = message;
            }

            public string PropertyName { get; }
            public string Message { get; }
        }
    }
}
