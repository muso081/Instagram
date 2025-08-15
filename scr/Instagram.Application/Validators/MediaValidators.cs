using FluentValidation;
using Instagram.Application.DTOs.MediaDtos;
namespace Instagram.Application.Validators;

public class MediaValidators : AbstractValidator<CreateMediaDto>
{
    public MediaValidators()
    {
        RuleFor(x => x.PostId)
            .GreaterThan(0).WithMessage("PostId must be greater than 0.");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Url must be a valid absolute URL.");

        RuleFor(x => x.MediaType)
            .IsInEnum().WithMessage("Invalid media type. Allowed values are Image or Video.");
    }
}
