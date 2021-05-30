using FluentValidation;

namespace MyCrm.Domain.Command.Todo
{
    internal class EditTodoCommandValidator : AbstractValidator<EditTodoCommand>
    {
        public EditTodoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Content)
                .MaximumLength(2048);
            RuleFor(x => x.ContactId)
                .NotEmpty();
        }
    }
}
