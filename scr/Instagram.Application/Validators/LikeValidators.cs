using FluentValidation;
using Instagram.Application.DTOs.LikeDtos;

namespace Instagram.Application.Validators;

public class LikeValidators : AbstractValidator<CreateLikeDto>
{
    public LikeValidators()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
          .GreaterThan(0).WithMessage("PostId must be greater than 0.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}
