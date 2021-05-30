using FluentValidation;

namespace MyCrm.Domain.Query.User
{
    class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
    {
        public SearchUsersQueryValidator()
        {
            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    if (value.PageNumber <= 0)
                    {
                        context.AddFailure("PageNumber", "Page number must be greater than 0.");
                    }

                    if (value.PageSize <= 0)
                    {
                        context.AddFailure("PageSize", "Page size must be greater than 0.");
                    }
                });
        }
    }
}
