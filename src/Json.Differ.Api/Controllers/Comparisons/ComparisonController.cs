using Json.Differ.Application.Files.CompareFilesDiffs;
using Json.Differ.Core.BadRequests;
using JsonDiffer.Api.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Json.Differ.Core.Apis.ResponseResult;

namespace JsonDiffer.Api.Controllers.Comparisons
{
    [DefaultRoute("diff/{externalId}")]
    public class ComparisonController : BaseController
    {
        public ComparisonController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(ComparisonDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarRecebimentoAsync(
        [FromRoute] Guid externalId,
        CancellationToken cancellationToken)
        {
            var request = new CompareFilesDiffsRequestDto
            {
                ExternalId = externalId
            };

            var responseDto = await Mediator.Send(request, cancellationToken);

            return ResponseToActionResult(responseDto, _ => responseDto.Response);
        }
    }
}