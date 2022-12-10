namespace Json.Differ.Core.NotificationResults
{
    public struct NotificationError
    {
        public NotificationError(string description, string? code = null)
        {
            Description = description;
            Code = code ?? "generic";
        }

        public string? Code { get; set; }
        public string Description { get; set; }

        public static implicit operator NotificationError(string description) => new NotificationError(description);
    }
}
