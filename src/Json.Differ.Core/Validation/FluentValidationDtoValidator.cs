using FluentValidation;
using Json.Differ.Core.Models;

namespace Json.Differ.Core.Validation
{
    public abstract class FluentValidationDtoValidator<TDto> :
    AbstractValidator<TDto>, IDtoValidator<TDto> where TDto : Dto
    {
        public virtual DtoValidationResult TryValidate(TDto target)
        {
            var valRes = Validate(target);
            var dtoValDets = valRes.Errors
                .Select(x => new DtoValidationDetail(x.ErrorMessage, x.PropertyName))
                    .ToList();

            var dtoValRes = new DtoValidationResult(valRes.IsValid, dtoValDets);
            return dtoValRes;
        }
    }
}