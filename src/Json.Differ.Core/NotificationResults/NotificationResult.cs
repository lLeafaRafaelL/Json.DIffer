using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.NotificationResults
{
    public struct NotificationResult
    {
        public bool Succeeded { get; set; }
        public IReadOnlyCollection<NotificationError> Errors { get; private set; }

        public static NotificationResult Success() =>
            new NotificationResult { Succeeded = true, Errors = new NotificationError[0] };

        public static NotificationResult Failure() =>
            new NotificationResult { Succeeded = false, Errors = new NotificationError[0] };

        public static NotificationResult Error(IEnumerable<NotificationError> failureDetails) =>
            new NotificationResult { Succeeded = false, Errors = failureDetails?.ToArray() ?? new NotificationError[0] };

        public static NotificationResult Error(params string[] failureDescriptions) =>
            Error(failureDetails: failureDescriptions?.Select(a => new NotificationError(a)) ?? new NotificationError[0]);
    }

    public struct NotificationResult<T>
    {
        public bool Succeeded { get; set; }
        public T Value { get; set; }
        public IReadOnlyCollection<NotificationError> Errors { get; private set; }

        public NotificationResult AsValueResult() => this ? NotificationResult.Success() : NotificationResult.Error(this.Errors);

        public static NotificationResult<T> Success(T value) =>
            new NotificationResult<T> { Succeeded = true, Value = value, Errors = new NotificationError[0] };

        public static NotificationResult<T> Failure() =>
            new NotificationResult<T> { Succeeded = false, Errors = new NotificationError[0] };

        public static NotificationResult<T> Error(IEnumerable<NotificationError> failureDetails) =>
            new NotificationResult<T> { Succeeded = false, Errors = failureDetails?.ToArray() ?? new NotificationError[0] };

        public static NotificationResult<T> Error(params string[] failureDescriptions) =>
            Error(failureDetails: failureDescriptions?.Select(a => new NotificationError(a)) ?? new NotificationError[0]);

        public static implicit operator bool(NotificationResult<T> valueResult) => valueResult.Succeeded;

        public static implicit operator NotificationResult(NotificationResult<T> valueResult) => valueResult.AsValueResult();
    }
}