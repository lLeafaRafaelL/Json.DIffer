using Json.Differ.Core.Handlers;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class CompareFilesDiffsResponseDto : ResponseDto
    {
        public ComparisonDto Response { get; set; }
    }
}
