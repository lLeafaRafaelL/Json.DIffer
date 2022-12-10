namespace Json.Differ.Core.Validation
{
    public class DtoValidationDetail
    {
        private string _memberName;

        public DtoValidationDetail(string message, string memberName)
        {
            Message = message;
            MemberName = memberName;
        }

        public DtoValidationDetail(string mensagem) :
            this(mensagem, null)
        { }

        public DtoValidationDetail() { }

        public string Message { get; set; }
        public string MemberName
        {
            get => string.IsNullOrEmpty(_memberName) ? "generic" : _memberName;
            set => _memberName = value;
        }
    }
}
