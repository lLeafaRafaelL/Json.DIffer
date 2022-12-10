using Json.Differ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Validation
{
    public interface IDtoValidator<in TDto> where TDto : Dto
    {
        DtoValidationResult TryValidate(TDto target);
    }
}