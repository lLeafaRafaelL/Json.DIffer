namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class ComparisonDto
    {
        public ComparisonDto()
        {
            LeftFileDiffs = new List<ComparisonFileDiffDto>();
            RightFileDiffs = new List<ComparisonFileDiffDto>();
        }

        public Guid ExternalId { get; set; }
        public string Result { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public IList<ComparisonFileDiffDto> LeftFileDiffs { get; set; }
        public IList<ComparisonFileDiffDto> RightFileDiffs { get; set; }

    }
}