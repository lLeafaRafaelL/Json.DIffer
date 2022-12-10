using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Json.Differ.Infrastructure.Context
{
    internal class DesignTimeDbContext : IDesignTimeDbContextFactory<JsonDifferContext>
    {
        public JsonDifferContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost, 1433;Database=JsonDiffer;User Id=sa;Password=Abc@12345; MultipleActiveResultSets=true;";
            var builder = new DbContextOptionsBuilder<JsonDifferContext>();

            builder
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();


            return new JsonDifferContext(builder.Options, new DbOptions(connectionString));
        }
    }
}