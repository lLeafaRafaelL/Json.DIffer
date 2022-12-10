using Json.Differ.Core.Models;
using Json.Differ.Domain.Files.Models;

namespace Json.Differ.Domain.Comparisons.Models
{
    public class ComparisonFile : Entity<Guid>
    {
        public ComparisonFile()
        {
            Id = Guid.NewGuid();
        }

        public ComparisonFile(Guid comparisonId, Guid fileId) : this()
        {
            ComparisonId = comparisonId;
            FileId = fileId;
        }

        public virtual Guid ComparisonId { get; private set; }
        public virtual Comparison Comparison { get; private set; }
        public virtual Guid FileId { get; private set; }
        public virtual FileToCompare File { get; private set; }
    }
}