using Json.Differ.Core.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Json.Differ.Core.Apis
{
    public class ResponseResult
    {
        public static IActionResult ResponseToActionResult<TResponse>(
            TResponse response,
            Func<TResponse, object> mapSuccessRequestBody,
            Func<TResponse, object> mapFailureRequestBody)
                where TResponse : ResponseDto
        {
            var httpStatus = response.Status;
            var mapRequestBody = response.Status.Succeeded()
                ? mapSuccessRequestBody
                : mapFailureRequestBody;

            var requestBody = mapRequestBody?.Invoke(response);
            var hasRequestBody = requestBody != null;
            var result = hasRequestBody
                ? new ObjectResult(requestBody) { StatusCode = (int)httpStatus }
                : (IActionResult)new StatusCodeResult((int)httpStatus);

            if (!response.Status.Succeeded() && hasRequestBody)
            {
                var options = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                });

            }
            return result;
        }

        public static IActionResult ResponseToActionResult<TCommandResponse>(
            TCommandResponse response,
            Func<TCommandResponse, object> mapSuccessRequestBody)
                where TCommandResponse : ResponseDto =>
                    ResponseToActionResult(response, mapSuccessRequestBody, x => x?.BadRequestReason);

        public static IActionResult ResponseToActionResult<TCommandResponse>(
            TCommandResponse response)
                where TCommandResponse : ResponseDto => ResponseToActionResult(response, null, x => x?.BadRequestReason);
    }
}