namespace Json.Differ.Infrastructure.Context
{
    public class DbOptions
    {
        public DbOptions(string connectionString)
        {
            ConnectionString = string.IsNullOrEmpty(connectionString) ?
                throw new ArgumentNullException(nameof(connectionString)) : connectionString;
        }

        public string ConnectionString { get; init; }
    }
}
