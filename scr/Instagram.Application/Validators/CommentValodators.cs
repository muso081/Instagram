using FluentValidation;
using Instagram.Application.DTOs.CommentDtos;

namespace Instagram.Application.Validators;

public class CommentValodators : AbstractValidator<CreateCommentDto>
{
    public CommentValodators()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
          .GreaterThan(0).WithMessage("PostId must be a positive number.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .GreaterThan(0).WithMessage("UserId must be a positive number.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(500).WithMessage("Content cannot exceed 500 characters.");
    }
}
