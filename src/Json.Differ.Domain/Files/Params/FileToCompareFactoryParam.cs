using Json.Differ.Domain.Files.Enums;

namespace Json.Differ.Domain.Files.Params
{
    public class FileToCompareFactoryParam
    {
        public virtual Guid ExternalId { get; set; }
        public virtual string DecodedFile { get; set; }
        public virtual FileSide Side { get; set; }
    }
}
