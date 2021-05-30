using FluentValidation;

namespace MyCrm.Domain.Command.Order
{
    internal class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Content)
                .MaximumLength(2048);
            RuleFor(x => x.Price)
                .NotEmpty();
            RuleFor(x => x.ContactId)
                .NotEmpty();
        }
    }
}
