using pipelineBehavior.Infra.Messaging;
using System;

namespace pipelineBehavior.Commands
{
    public sealed record CreateUserCommand(string Name, string Document) : ICommand<Guid>;
}