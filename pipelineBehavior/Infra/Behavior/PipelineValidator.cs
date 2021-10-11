using FluentValidation;
using MediatR;
using pipelineBehavior.Infra.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pipelineBehavior.Infra.Behavior
{
    public sealed class PipelineValidator<TRequest, TResponse> : IPipeline<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public PipelineValidator(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators.Select(x => x.Validate(context))
                                                          .SelectMany(x => x.Errors)
                                                          .Where(x => x != null)
                                                          .GroupBy(
                                                                x => x.PropertyName,
                                                                x => x.ErrorMessage,
                                                                (propertyName, errorMessages) => new
                                                                {
                                                                    Key = propertyName,
                                                                    Values = errorMessages.Distinct().ToArray()
                                                                }).ToDictionary(x => x.Key, x => x.Values);

            if (errors.Any()) throw new Exceptions.ValidationException(errors);

            return await next();
        }
    }
}