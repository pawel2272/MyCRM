using FluentValidation;

namespace MyCrm.Domain.Command.Order
{
    internal class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
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
