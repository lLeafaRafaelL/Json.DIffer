using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Mapping
{
    public interface IMapper<in TSource, TTarget>
        where TSource : class
        where TTarget : class, new()
    {
        void Map(TSource source, TTarget target);
        TTarget Map(TSource source);
    }
}
