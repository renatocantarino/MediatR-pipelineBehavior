using System;

namespace pipelineBehavior.Infra.Messaging
{
    public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
    {
        Guid RequestId { get; set; }
    }
}