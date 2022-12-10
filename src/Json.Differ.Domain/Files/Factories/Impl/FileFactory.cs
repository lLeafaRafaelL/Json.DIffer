using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Domain.Files.Repositories;
using Json.Differ.Core.NotificationResults;
using Newtonsoft.Json.Linq;

namespace Json.Differ.Domain.Files.Factories.Impl
{
    public class FileFactory : IFileFactory
    {
        private readonly IFileRepository _repository;

        public FileFactory(IFileRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), typeof(FileFactory).FullName);
        }

        public async ValueTask<NotificationResult<FileToCompare>> CreateAsync(FileToCompareFactoryParam factoryParam, CancellationToken cancellationToken)
        {
            try
            {
                try
                {
                    var jsonObj = JObject.Parse(factoryParam.DecodedFile);
                }
                catch (Exception e)
                {
                    return NotificationResult<FileToCompare>.Error("Invalid Json", e.Message);
                }          

                var existsFile = await _repository.GetAsync(a => a.ExternalId == factoryParam.ExternalId && a.Side == factoryParam.Side, cancellationToken);
                if (existsFile != null)
                    return NotificationResult<FileToCompare>.Error($"A file aready exists - External Id: {factoryParam.ExternalId} - Side: {factoryParam.Side.ToString()}");

                var file = new FileToCompare(factoryParam);
                return NotificationResult<FileToCompare>.Success(file);
            }
            catch (Exception ex)
            {
                return NotificationResult<FileToCompare>.Error(ex.Message);
            }
        }
    }
}