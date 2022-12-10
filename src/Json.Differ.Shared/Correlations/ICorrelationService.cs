using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Shared.Correlations
{
    public interface ICorrelationService
    {
        Guid GetCorrelationId();
        void SetCorrelationId(Guid correlationId);
    }
}
