using Json.Differ.Core.BadRequests;
using System.Net;

namespace Json.Differ.Core.Handlers
{
    public static class Responses
    {
        public static T OK<T>(Action<T> act = null)
            where T : ResponseDto, new() =>
                CreateResponse(HttpStatusCode.OK, act);


        public static T Created<T>(Action<T> act = null)
            where T : ResponseDto, new() =>
                CreateResponse(HttpStatusCode.Created, act);


        public static T Accepted<T>(Action<T> act = null)
            where T : ResponseDto, new() =>
                CreateResponse(HttpStatusCode.Accepted, act);


        public static T NoContent<T>(Action<T> act = null)
            where T : ResponseDto, new() =>
                CreateResponse(HttpStatusCode.NoContent, act);


        public static T BadRequest<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : ResponseDto, new()
        {
            var resp = CreateResponse(HttpStatusCode.BadRequest, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T Unauthorized<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : ResponseDto, new()
        {
            var resp = CreateResponse(HttpStatusCode.Unauthorized, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        public static T NotFound<T>(
            BadRequestDto badReqReason = null, Action<T> act = null)
                where T : ResponseDto, new()
        {
            var resp = CreateResponse(HttpStatusCode.NotFound, act);
            resp.BadRequestReason = badReqReason;
            return resp;
        }

        private static T CreateResponse<T>(
            HttpStatusCode status, Action<T> act = null)
                where T : ResponseDto, new()
        {
            var resp = new T { Status = status };
            act?.Invoke(resp);
            return resp;
        }
    }
}