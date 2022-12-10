using System.Net;

namespace Json.Differ.Core.Apis
{
    public static class ResponseStatusExtensions
    {
        public static bool Succeeded(this HttpStatusCode @this) =>
            @this == HttpStatusCode.OK ||
            @this == HttpStatusCode.Created ||
            @this == HttpStatusCode.Accepted ||
            @this == HttpStatusCode.NoContent;
    }
}