using Json.Differ.Core.Models;

namespace Json.Differ.Core.BadRequests
{
    public class BadRequestDetailDto : Dto
    {
        private string _code;

        public BadRequestDetailDto() { }
        public BadRequestDetailDto(string descricao, string code)
        {
            Description = descricao;
            Code = code ?? "generic";
        }

        public string Description { get; set; }
        public string Code
        {
            get => _code;
            set => _code = value;
        }
    }
}