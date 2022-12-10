using Json.Differ.Domain.Files.Enums;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class ComparisonFileDiffDto
    {
        public  string Field { get; set; }
        public  string Value { get; set; }
        public  int ValueLenth => Value.Length;
        public  int Lenth => (Field + Value).Length;
    }
}
