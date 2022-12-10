using FluentValidation;
using Json.Differ.Core.Validation;
using static JsonDiffer.Shared.Validation.ValidationMessages;

namespace Json.Differ.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareValidator : FluentValidationDtoValidator<UploadFileToCompareRequestDto>
    {
        public UploadFileToCompareValidator()
        {
            RuleFor(a => a.ExternalId)
                .NotEmpty()
                .WithMessage(NotNullOrEmptyMessage);

            RuleFor(a => a.EncodedFile)
                .NotEmpty()
                .WithMessage(NotNullOrEmptyMessage);
        }
    }
}