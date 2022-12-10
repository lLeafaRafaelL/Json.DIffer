using Json.Differ.Core.BadRequests;
using Json.Differ.Core.Models;
using System.Net;

namespace Json.Differ.Core.Handlers
{
    public abstract class ResponseDto : Dto
    {
        protected ResponseDto()
        {
            Status = HttpStatusCode.OK;
        }

        public virtual HttpStatusCode Status { get; set; }
        public virtual BadRequestDto BadRequestReason { get; set; }
    }
}