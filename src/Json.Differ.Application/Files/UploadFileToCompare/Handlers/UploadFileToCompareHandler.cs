using Json.Differ.Core.Data;
using Json.Differ.Core.Handlers;
using Json.Differ.Core.Mapping;
using Json.Differ.Core.Validation;
using Json.Differ.Domain.Files.Factories;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Domain.Files.Repositories;
using Json.Differ.Shared.Extensions;
using Newtonsoft.Json.Linq;
using static Json.Differ.Core.BadRequests.BadRequests;
using static Json.Differ.Core.Handlers.Responses;

namespace Json.Differ.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareHandler : IHandler<UploadFileToCompareRequestDto, UploadFileToCompareResonseDto>
    {
        private readonly IFileRepository _repository;
        private readonly IFileFactory _factory;
        private readonly IUnityOfWorkAsync _unitOfWork;
        private readonly IDtoValidator<UploadFileToCompareRequestDto> _validator;

        public UploadFileToCompareHandler(
            IFileRepository repository, 
            IFileFactory factory, 
            IUnityOfWorkAsync unitOfWork, 
            IDtoValidator<UploadFileToCompareRequestDto> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), typeof(UploadFileToCompareHandler).FullName);
            _factory = factory ?? throw new ArgumentNullException(nameof(factory), typeof(UploadFileToCompareHandler).FullName);
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), typeof(UploadFileToCompareHandler).FullName);
            _validator = validator ?? throw new ArgumentNullException(nameof(validator), typeof(UploadFileToCompareHandler).FullName);
        }

        public async Task<UploadFileToCompareResonseDto> Handle(UploadFileToCompareRequestDto request, CancellationToken cancellationToken)
        {
            if (cancellationToken == CancellationToken.None)
                throw new ArgumentNullException(nameof(cancellationToken));

            var validationResult = _validator.TryValidate(request);
            if (!validationResult.Succeeded)
                return BadRequest<UploadFileToCompareResonseDto>(
                    BadRequestReasonFrom(validationResult.Details));

            var decodeFile = request.EncodedFile.DecodeBase64();
            if (!decodeFile.Succeeded)
                return BadRequest<UploadFileToCompareResonseDto>(
                    BadRequestReasonFrom(decodeFile.Errors));

            var fileValue = await _factory.CreateAsync(
                new FileToCompareFactoryParam
                {
                    ExternalId = request.ExternalId,
                    Side = request.Side,
                    DecodedFile = decodeFile.Value
                }, 
                cancellationToken);

            if (!fileValue.Succeeded)
                return BadRequest<UploadFileToCompareResonseDto>(
                    BadRequestReasonFrom(fileValue.Errors));

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _repository.AddAsync(fileValue.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Created<UploadFileToCompareResonseDto>(a => a.Response = 
                new FileUploadedDto
                {
                    ExternalId = fileValue.Value.ExternalId,
                    UploadedOn = fileValue.Value.CreatedOn
                });
        }
    }
}