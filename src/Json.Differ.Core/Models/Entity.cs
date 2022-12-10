using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Models
{
    public abstract class Entity
    {
        public Entity()
        {
            CreatedOn = UpdatedOn = DateTimeOffset.UtcNow;
        }

        public abstract object UntypedId { get; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
    }

    public abstract class Entity<TId> : Entity
    {

        public TId Id { get; set; }
        public override object UntypedId => Id;
    }
}