using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
                
        builder.Property(d => d.Address)
            .HasMaxLength(200);
                
        builder.Property(d => d.City)
            .HasMaxLength(100);
                
        builder.Property(d => d.ContactPerson)
            .HasMaxLength(100);
                
        builder.Property(d => d.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.UpdatedBy)
            .HasMaxLength(450);

        builder.Property(d => d.UpdatedAt);

        builder.Property(d => d.DeletedBy)
            .HasMaxLength(450);

        builder.Property(d => d.DeletedAt);

        builder.Property(d => d.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasQueryFilter(d => !d.IsDeleted);
        
        builder.ToTable("Destinations");
    }
}
