using Json.Differ.Core.Handlers;
using MediatR;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class CompareFilesDiffsRequestDto : RequestDto, IRequest<CompareFilesDiffsResponseDto>
    {
        public Guid ExternalId { get; set; }
    }
}
