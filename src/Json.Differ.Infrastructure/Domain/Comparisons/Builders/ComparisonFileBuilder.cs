using Json.Differ.Domain.Comparisons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Json.Differ.Infrastructure.Domain.Comparisons.Builders
{
    internal class ComparisonFileBuilder
    {
        public void Build(EntityTypeBuilder<ComparisonFile> builder)
        {
            builder.HasKey(a => a.Id)
                   .IsClustered(false);

            builder.Property(x => x.CreatedOn)
                   .IsRequired();

            builder.Property(x => x.UpdatedOn)
                   .IsRequired();

            builder.Property(x => x.FileId)
                   .IsRequired();

            builder.HasOne(a => a.Comparison)
                   .WithMany(a => a.Files)
                   .HasForeignKey(a => a.ComparisonId);

            builder.HasOne(a => a.File)
                   .WithOne()
                   .HasForeignKey<ComparisonFile>(b => b.FileId);
        }
    }
}