using BookRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRental.Infrastructure.Data.Configuration;

public class RentConfiguration : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {

        builder.HasKey(r => r.RentId);
        
        builder.Property(r => r.RentId)
            .UseIdentityColumn()
            .IsRequired();
        
        builder.Property(r => r.RentDate)
            .IsRequired();
                
        builder.Property(r => r.DueDate)
            .IsRequired();
                
        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.HasOne(r => r.Book)
            .WithMany(b => b.Rents)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Rents)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(r => r.Destination)
            .WithMany(d => d.Rents)
            .HasForeignKey(r => r.DestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Rents");
        
    }
}