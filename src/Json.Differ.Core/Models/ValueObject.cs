using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Models
{
    public abstract class ValueObject
    {
        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
    }
}
