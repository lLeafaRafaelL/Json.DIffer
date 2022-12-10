using Json.Differ.Core.Validation;
using Json.Differ.Core.NotificationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.BadRequests
{
    public static class BadRequests
    {
        public static BadRequestDto BadRequestReasonFrom(string description) =>
            new BadRequestDto { new BadRequestDetailDto(description, null) };

        public static BadRequestDto BadRequestReasonFrom(
            IEnumerable<string> descriptions) =>
                new BadRequestDto(descriptions?.Select(x => new BadRequestDetailDto(x, null)));

        public static BadRequestDto BadRequestReasonFrom(
            params string[] descricoes) =>
                BadRequestReasonFrom((IEnumerable<string>)descricoes);

        public static BadRequestDto BadRequestReasonFrom(
            IEnumerable<DtoValidationDetail> dtoValidationDets) =>
                new BadRequestDto(dtoValidationDets?
                    .Select(x => new BadRequestDetailDto(x.Message, x.MemberName)));

        public static BadRequestDto BadRequestReasonFrom(
            IReadOnlyCollection<NotificationError> failureDetails) =>
                new BadRequestDto(failureDetails?.Select(x => new BadRequestDetailDto(x.Description, x.Code)));
    }
}