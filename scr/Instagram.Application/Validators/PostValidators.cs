using FluentValidation;
using Instagram.Application.DTOs.PostDtos;
namespace Instagram.Application.Validators;

public class PostValidators : AbstractValidator<CreatePostDto>
{
    public PostValidators()
    {
        RuleFor(x => x.UserId)
           .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.Caption)
            .NotEmpty().WithMessage("Caption is required.")
            .MaximumLength(500).WithMessage("Caption must not exceed 500 characters.");

        RuleFor(x => x.Media)
            .NotNull().WithMessage("Media list cannot be null.")
            .NotEmpty().WithMessage("At least one media item is required.");

        RuleForEach(x => x.Media)
            .SetValidator(new MediaValidators());
    }
}
