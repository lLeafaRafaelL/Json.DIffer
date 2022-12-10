namespace Json.Differ.Core.Validation
{
    public class DtoValidationResult
    {
        private static readonly
            IReadOnlyCollection<DtoValidationDetail> EmptyDetails =
                new DtoValidationDetail[0];

        private IReadOnlyCollection<DtoValidationDetail> _details;

        public DtoValidationResult(
            bool succeeded, IReadOnlyCollection<DtoValidationDetail> details)
        {
            Succeeded = succeeded;
            Details = details;
        }

        public DtoValidationResult(bool succeeded) : this(succeeded, null) { }

        public bool Succeeded { get; private set; }
        public IReadOnlyCollection<DtoValidationDetail> Details
        {
            get => _details ?? EmptyDetails;
            private set => _details = value;
        }
    }
}