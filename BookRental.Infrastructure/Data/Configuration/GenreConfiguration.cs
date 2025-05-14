using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.GenreId);
        
        builder.Property(g => g.GenreId)
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.ToTable("Genres");

    }
}