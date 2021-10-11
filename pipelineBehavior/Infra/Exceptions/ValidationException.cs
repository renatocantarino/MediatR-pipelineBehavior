using System;
using System.Collections.Generic;

namespace pipelineBehavior.Infra.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string title, string message)
            : base(message) =>
            Title = title;

        public string Title { get; }
    }

    public sealed class ValidationException : ApplicationException
    {
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validator", "One or more validation has errors")

       => ErrorsDictionary = errors;
    }

    public abstract class BadRequestException : ApplicationException
    {
        protected BadRequestException(string message)
            : base("Bad Request", message)
        {
        }
    }

    public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }

    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId)
            : base($"The user with the identifier {userId} was not found.")
        {
        }
    }
}