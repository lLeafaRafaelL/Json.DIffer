using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Models
{
    public abstract class AgregateRoot : Entity<Guid>
    {
        public virtual long SequentialKey { get; set; }
        public virtual byte[] Timestamp { get; set; }
    }
}