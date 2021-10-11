using FluentValidation;
using pipelineBehavior.Commands;

namespace pipelineBehavior.Validations
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(250);

            RuleFor(x => x.Document).NotEmpty()
                    .NotEmpty()
                    .MaximumLength(100);
        }
    }
}