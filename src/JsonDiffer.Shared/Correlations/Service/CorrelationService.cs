using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDiffer.Shared.Correlations.Service
{
    public sealed class CorrelationService : ICorrelationService
    {

        private Guid _correlationId { get; set; }

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }

        public void SetCorrelationId(Guid correlationId)
        {
            _correlationId = correlationId;
        }

    }
}