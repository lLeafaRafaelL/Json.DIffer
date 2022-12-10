using FluentValidation;
using Json.Differ.Core.Validation;
using static Json.Differ.Shared.Validation.ValidationMessages;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class CompareFilesDiffsRequestValidator : FluentValidationDtoValidator<CompareFilesDiffsRequestDto>
    {
        public CompareFilesDiffsRequestValidator()
        {
            RuleFor(a => a.ExternalId)
                .NotEmpty()
                .WithMessage(NotNullOrEmptyMessage);
        }
    }
}