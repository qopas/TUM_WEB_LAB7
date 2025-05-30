using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);
            
        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500);
                
        builder.Property(rt => rt.JwtId)
            .IsRequired()
            .HasMaxLength(256);
                
        builder.Property(rt => rt.CreationDate)
            .IsRequired();
                
        builder.Property(rt => rt.ExpiryDate)
            .IsRequired();
                
        builder.Property(rt => rt.Used)
            .IsRequired()
            .HasDefaultValue(false);
                
        builder.Property(rt => rt.Invalidated)
            .IsRequired()
            .HasDefaultValue(false);
                
        builder.Property(rt => rt.UserId)
            .IsRequired();

        builder.HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder.HasQueryFilter(rt => !rt.IsDeleted);
                
        builder.ToTable("RefreshTokens");
    }
}
