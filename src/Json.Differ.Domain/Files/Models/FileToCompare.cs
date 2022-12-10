using Json.Differ.Core.Models;
using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Params;

namespace Json.Differ.Domain.Files.Models
{
    public class FileToCompare : AgregateRoot
    {
        internal FileToCompare()
        {
            Id = Guid.NewGuid();
            Type = DataType.JSON;
        }

        public FileToCompare(FileToCompareFactoryParam factoryParam) : this()
        {
            ExternalId = factoryParam.ExternalId;
            Data = factoryParam.DecodedFile;
            Side = factoryParam.Side;
        }

        public virtual Guid ExternalId { get; private set; }
        public virtual string Data { get; private set; }
        public virtual DataType Type { get; private set; }
        public virtual FileSide Side { get; private set; }

        public bool IsLeftSide => Side == FileSide.LEFT;
        public bool IsRightSide => Side == FileSide.RIGHT;
    }
}