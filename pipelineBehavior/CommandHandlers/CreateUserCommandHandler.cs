using pipelineBehavior.Commands;
using pipelineBehavior.Infra.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace pipelineBehavior.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}