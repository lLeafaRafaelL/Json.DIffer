using Json.Differ.Core.Data;
using Json.Differ.Core.Handlers;
using Json.Differ.Core.Mapping;
using Json.Differ.Core.Validation;
using Json.Differ.Domain.Comparisons.Factories;
using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Comparisons.Repositories;
using static Json.Differ.Core.BadRequests.BadRequests;
using static Json.Differ.Core.Handlers.Responses;

namespace Json.Differ.Application.Files.CompareFilesDiffs
{
    public class CompareFilesDiffsHandler : IHandler<CompareFilesDiffsRequestDto, CompareFilesDiffsResponseDto>
    {
        private readonly IComparisonRepository _repository;
        private readonly IComparisonFactory _factory;
        private readonly IUnityOfWorkAsync _unitOfWork;
        private readonly IDtoValidator<CompareFilesDiffsRequestDto> _validator;
        private readonly IMapper<Comparison, ComparisonDto> _mapper;

        public CompareFilesDiffsHandler(
            IComparisonRepository repository, 
            IComparisonFactory factory, 
            IUnityOfWorkAsync unitOfWork, 
            IDtoValidator<CompareFilesDiffsRequestDto> validator, 
            IMapper<Comparison, ComparisonDto> mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), typeof(CompareFilesDiffsHandler).FullName);
            _factory = factory ?? throw new ArgumentNullException(nameof(factory), typeof(CompareFilesDiffsHandler).FullName);
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), typeof(CompareFilesDiffsHandler).FullName);
            _validator = validator ?? throw new ArgumentNullException(nameof(validator), typeof(CompareFilesDiffsHandler).FullName);
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), typeof(CompareFilesDiffsHandler).FullName);
        }

        public async Task<CompareFilesDiffsResponseDto> Handle(CompareFilesDiffsRequestDto request, CancellationToken cancellationToken)
        {
            if (cancellationToken == CancellationToken.None)
                throw new ArgumentNullException(nameof(cancellationToken));

            var validationResult = _validator.TryValidate(request);
            if (!validationResult.Succeeded)
                return BadRequest<CompareFilesDiffsResponseDto>(
                    BadRequestReasonFrom(validationResult.Details));

            //if exists a comparison with that external id, don't compare again, return the comparison results.
            var existsComparison = await _repository.GetAsync(a => a.ExternalId == request.ExternalId, cancellationToken);
            if (existsComparison != null)
                return OK<CompareFilesDiffsResponseDto>(a => a.Response = _mapper.Map(existsComparison));

            var comparison = await _factory.CreateAsync(request.ExternalId, cancellationToken);
            if (!comparison.Succeeded)
                return BadRequest<CompareFilesDiffsResponseDto>(
                    BadRequestReasonFrom(comparison.Errors));

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _repository.AddAsync(comparison.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Created<CompareFilesDiffsResponseDto>(a => a.Response = _mapper.Map(comparison.Value));
        }
    }
}