using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.HasKey(c => c.CustomerId);
        
        builder.Property(c => c.CustomerId)
            .UseIdentityColumn()
            .IsRequired();
        
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);
                
        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);
                
        builder.Property(c => c.Email)
            .HasMaxLength(100);
                
        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(20);
                
        builder.Property(c => c.Address)
            .HasMaxLength(200);
                
        builder.Property(c => c.City)
            .HasMaxLength(100);
        
        builder.ToTable("Customers");
        
    }
}