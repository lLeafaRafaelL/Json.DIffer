using FluentValidation;
using Json.Differ.Core.Validation;
using static JsonDiffer.Shared.Validation.ValidationMessages;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class CompareFilesDiffsRequestValidator : FluentValidationDtoValidator<CompareFilesDiffsRequestDto>
    {
        public CompareFilesDiffsRequestValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty()
                .WithMessage(NotNullOrEmptyMessage);
        }
    }
}