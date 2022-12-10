using Json.Differ.Domain.Comparisons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Json.Differ.Infrastructure.Domain.Comparisons.Builders
{
    internal class ComparisonBuilder
    {
        public void Build(EntityTypeBuilder<Comparison> builder)
        {
            builder.HasKey(a => a.Id)
                   .IsClustered(false);

            builder.Property(a => a.SequentialKey)
                   .ValueGeneratedOnAdd();
            builder.HasIndex(a => a.SequentialKey)
                   .IsUnique();

            builder.Property(x => x.Timestamp)
                   .IsRowVersion();

            builder.Property(x => x.CreatedOn)
                   .IsRequired();

            builder.Property(x => x.UpdatedOn)
                   .IsRequired();

            builder.Property(x => x.Result)
                   .IsRequired(false)
                   .HasColumnType("varchar")
                   .HasMaxLength(150);

            builder.Property(x => x.ExternalId)
                   .IsRequired();

            builder.HasMany(a => a.Files)
                   .WithOne(a => a.Comparison);

            builder.HasMany(a => a.FilesDiffs)
                   .WithOne(a => a.Comparison);

            builder.Ignore(a => a.LeftFileDiffs);
            builder.Ignore(a => a.RightFileDiffs);
        }
    }
}