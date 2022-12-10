using Json.Differ.Core.NotificationResults;
using Json.Differ.Domain.Comparisons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Domain.Comparisons.Factories
{
    public interface IComparisonFactory
    {
        ValueTask<NotificationResult<Comparison>> CreateAsync(Guid externalId, CancellationToken cancellationToken);
    }
}
