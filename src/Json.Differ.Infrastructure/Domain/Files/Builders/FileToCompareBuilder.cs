using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Json.Differ.Infrastructure.Domain.Files.Builders
{
    internal class FileToCompareBuilder
    {
        public void Build(EntityTypeBuilder<FileToCompare> builder)
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

            builder.Property(x => x.Data)
                   .IsRequired(true)
                   .HasColumnType("text");

            builder.Property(x => x.ExternalId)
                   .IsRequired();

            builder.Property(x => x.Side)
                   .IsRequired()
                   .HasConversion(b => b.ToString(), c => (FileSide)Enum.Parse(typeof(FileSide), c))
                   .HasColumnType("varchar")
                   .HasMaxLength(20);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion(b => b.ToString(), c => (DataType)Enum.Parse(typeof(DataType), c))
                   .HasColumnType("varchar")
                   .HasMaxLength(20);
        }
    }
}