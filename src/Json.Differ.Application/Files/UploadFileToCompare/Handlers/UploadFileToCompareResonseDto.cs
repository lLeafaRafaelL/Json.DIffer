using Json.Differ.Core.Handlers;

namespace Json.Differ.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareResonseDto : ResponseDto
    {
        public FileUploadedDto Response { get; set; }
    }
}
