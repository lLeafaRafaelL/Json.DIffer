using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Comparisons.Repositories;
using Json.Differ.Domain.Files.Repositories;
using Json.Differ.Core.NotificationResults;

namespace Json.Differ.Domain.Comparisons.Factories.Impl
{
    public class ComparisonFactory : IComparisonFactory
    {
        private readonly IComparisonRepository _repository;
        private readonly IFileRepository _fileRepository;

        public ComparisonFactory(IComparisonRepository repository, IFileRepository fileRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), typeof(ComparisonFactory).FullName);
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository), typeof(ComparisonFactory).FullName);
        }

        public async ValueTask<NotificationResult<Comparison>> CreateAsync(Guid externalId, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _repository.GetAsync(a => a.ExternalId == externalId, cancellationToken);
                if (exists != null)
                    return NotificationResult<Comparison>.Error($"A compraison already exists - Id: {externalId}");

                var files = await _fileRepository.FindAsync(a =>a.ExternalId == externalId, cancellationToken);
                if (!files.Any(a => a.IsLeftSide))
                    return NotificationResult<Comparison>.Error($"Left file to compare is missing");

                if (!files.Any(a => a.IsRightSide))
                    return NotificationResult<Comparison>.Error($"Right file to compare is missing");

                var comparison = new Comparison(
                    externalId, 
                    files);

                return NotificationResult<Comparison>.Success(comparison);
            }
            catch (Exception ex)
            {
                return NotificationResult<Comparison>.Error(ex.Message);
            }
        }
    }
}