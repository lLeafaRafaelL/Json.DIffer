using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Files.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Json.Differ.Infrastructure.Domain.Comparisons.Builders
{
    internal class ComparisonFileDiffBuilder
    {
        public void Build(EntityTypeBuilder<ComparisonFileDiff> builder)
        {
            builder.HasKey(a => a.Id)
                   .IsClustered(false);

            builder.Property(x => x.CreatedOn)
                   .IsRequired();

            builder.Property(x => x.UpdatedOn)
                   .IsRequired();

            builder.Property(x => x.Field)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(200);

            builder.Property(x => x.Value)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(500);

            builder.Property(x => x.FileSide)
                   .IsRequired()
                   .HasConversion(b => b.ToString(), c => (FileSide)Enum.Parse(typeof(FileSide), c))
                   .HasColumnType("varchar")
                   .HasMaxLength(20);

            builder.HasOne(a => a.Comparison)
                   .WithMany(a => a.FilesDiffs)
                   .HasForeignKey(a => a.ComparisonId);
        }
    }
}
