using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
            
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);
                    
        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);
                    
        builder.Property(c => c.Address)
            .HasMaxLength(200);
                    
        builder.Property(c => c.City)
            .HasMaxLength(100);
            
        builder.Property(c => c.ApplicationUserId)
            .IsRequired(false);

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedBy)
            .HasMaxLength(450);

        builder.Property(c => c.UpdatedAt);

        builder.Property(c => c.DeletedBy)
            .HasMaxLength(450);

        builder.Property(c => c.DeletedAt);

        builder.Property(c => c.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(c => c.ApplicationUser)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(c => !c.IsDeleted);
            
        builder.ToTable("Customers");
    }
}
