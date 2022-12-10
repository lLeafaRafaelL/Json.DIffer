using Json.Differ.Core.Handlers;
using Json.Differ.Domain.Files.Enums;
using MediatR;

namespace Json.Differ.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareRequestDto : RequestDto, IRequest<UploadFileToCompareResonseDto>
    {
        public Guid ExternalId { get; set; }
        public string EncodedFile { get; set; }
        public FileSide Side { get; set; }
    }
}
