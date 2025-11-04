using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
           builder.HasKey(c => c.Id);

            // Relationship: Customer -> CustomerInvoices
            builder.HasMany(c => c.Invoices)
                   .WithOne(ci => ci.Customer)
                   .HasForeignKey(ci => ci.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: constraints
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            
        }
    }
}