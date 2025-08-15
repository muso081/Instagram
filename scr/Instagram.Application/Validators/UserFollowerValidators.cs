using FluentValidation;
using Instagram.Domain.Entities;

namespace Instagram.Application.Validators;

public class UserFollowerValidators : AbstractValidator<UserFollower>
{
    public UserFollowerValidators()
    {
        RuleFor(x => x.UserId)
              .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.FollowedUserId)
            .GreaterThan(0).WithMessage("FollowedUserId must be greater than 0.");

        RuleFor(x => x)
            .Must(x => x.UserId != x.FollowedUserId)
            .WithMessage("User cannot follow themselves.");
    }
}

