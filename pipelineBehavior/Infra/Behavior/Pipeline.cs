using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace pipelineBehavior.Infra.Behavior
{
    public interface IPipeline<in TRequest, TResponse> where TRequest : notnull
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
    }
}