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

        builder.HasQueryFilter(d => !d.IsDeleted);
        
        builder.ToTable("Destinations");
    }
}
