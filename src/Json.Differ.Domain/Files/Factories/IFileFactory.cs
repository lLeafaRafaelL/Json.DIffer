using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Core.NotificationResults;
using System.Threading.Tasks;

namespace Json.Differ.Domain.Files.Factories
{
    public interface IFileFactory
    {
        ValueTask<NotificationResult<FileToCompare>> CreateAsync(FileToCompareFactoryParam factoryParam, CancellationToken cancellationToken);
    }
}
