using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Infrastructure.Domain.Comparisons.Builders;
using Json.Differ.Infrastructure.Domain.Files.Builders;
using Microsoft.EntityFrameworkCore;

namespace Json.Differ.Infrastructure.Context
{
    public class JsonDifferContext : DbContext
    {
        private readonly DbOptions _dbOptions;

        public JsonDifferContext
        (
            DbContextOptions<JsonDifferContext> options,
            DbOptions dbOptions

        ) : base(options)
        {
            _dbOptions = dbOptions ?? throw new ArgumentNullException(nameof(dbOptions));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_dbOptions.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ComparisonBuilder().Build(modelBuilder.Entity<Comparison>());
            new ComparisonFileBuilder().Build(modelBuilder.Entity<ComparisonFile>());
            new ComparisonFileDiffBuilder().Build(modelBuilder.Entity<ComparisonFileDiff>());
            new FileToCompareBuilder().Build(modelBuilder.Entity<FileToCompare>());

            base.OnModelCreating(modelBuilder);
        }
    }
}