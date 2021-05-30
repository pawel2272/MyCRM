using FluentValidation;

namespace MyCrm.Domain.Command.Contact
{
    internal class EditContactCommandValidator : AbstractValidator<EditContactCommand>
    {
        public EditContactCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);
            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.ContactComment)
                .MaximumLength(2048);
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
