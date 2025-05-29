using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.CreatedBy)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(g => g.CreatedAt)
            .IsRequired();
        
        builder.ToTable("Genres");
    }
}
