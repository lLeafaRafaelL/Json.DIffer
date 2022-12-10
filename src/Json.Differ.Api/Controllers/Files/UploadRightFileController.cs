using Json.Differ.Application.Files.UploadFileToCompare;
using Json.Differ.Core.BadRequests;
using JsonDiffer.Api.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Json.Differ.Core.Apis.ResponseResult;

namespace JsonDiffer.Api.Controllers.Files
{
    [DefaultRoute("diff/{externalId}/right")]
    public class UploadRightFileController : BaseController
    {
        public UploadRightFileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(FileUploadedDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UploadFileAsync(
        [FromRoute] Guid externalId,
        [FromBody] FileToUploadDto file,
        CancellationToken cancellationToken)
        {
            var request = new UploadFileToCompareRequestDto
            {
                ExternalId = externalId,
                EncodedFile = file.EncodedFile,
                Side = Json.Differ.Domain.Files.Enums.FileSide.RIGHT
            };

            var responseDto = await Mediator.Send(request, cancellationToken);

            return ResponseToActionResult(responseDto, _ => responseDto.Response);
        }
    }
}