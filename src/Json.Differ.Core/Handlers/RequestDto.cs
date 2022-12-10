using Json.Differ.Core.Models;

namespace Json.Differ.Core.Handlers
{
    public abstract class RequestDto : Dto
    {
        private readonly Dictionary<string, string> _headers;
        public RequestDto()
        {
            _headers = new Dictionary<string, string>();
        }
        public IReadOnlyDictionary<string, string> Headers
        {
            get { return _headers; }
        }
        public virtual Guid CorrelationId { get; set; } = Guid.NewGuid();

        public void AddHeader(string key, string value)
        {
            if (_headers.Any(x => x.Key.Equals(key))) _headers.Remove(key);

            _headers.Add(key, value);
        }
    }
}