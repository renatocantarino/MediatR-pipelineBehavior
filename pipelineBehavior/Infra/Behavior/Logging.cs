using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace pipelineBehavior.Infra.Behavior
{
    public class Logging<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<Logging<TRequest, TResponse>> _logger;

        public Logging(ILogger<Logging<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            IList<PropertyInfo> props = new List<PropertyInfo>(request.GetType().GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }

            var response = await next();

            _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}