using Json.Differ.Core.Models;
using Json.Differ.Domain.Files.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Domain.Comparisons.Models
{
    public class ComparisonFileDiff : Entity<Guid>
    {
        public ComparisonFileDiff()
        {
            Id = Guid.NewGuid();
        }

        public ComparisonFileDiff(Guid comparisonId, FileSide fileSide, string field, string value) : this()
        {
            ComparisonId = comparisonId;
            FileSide = fileSide;
            Field = field;
            Value = value;
        }

        public virtual string Field { get; set; }
        public virtual string Value { get; private set; }
        public virtual int ValueLenth => Value.Length;
        public virtual int Lenth => (Field + Value).Length;
        public virtual FileSide FileSide { get; private set; }
        public virtual Guid ComparisonId { get; private set; }
        public virtual Comparison Comparison{ get; private set; }

        public bool IsLeftSide => FileSide == FileSide.LEFT;
        public bool IsRightSide => FileSide == FileSide.RIGHT;
    }
}