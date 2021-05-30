using FluentValidation;

namespace MyCrm.Domain.Command.User
{
    internal class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty();
            RuleFor(x => x.Gender)
                .NotEmpty()
                .MaximumLength(1);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(48);
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
            RuleFor(x => x.RoleId)
                .NotEmpty();
        }
    }
}
