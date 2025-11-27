using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            // Relationship: Product -> MenuItems
            builder.HasMany(p => p.MenuItems)
                   .WithOne(mi => mi.Product)
                   .HasForeignKey(mi => mi.ProductId);

            // Relationship: Product -> Prices
            builder.HasMany(p => p.Prices)
                   .WithOne(pr => pr.Product)
                   .HasForeignKey(pr => pr.ProductId);

            // Relationship: Product -> CustomerInvoiceItems
            builder.HasMany(p => p.InvoiceItems)
                   .WithOne(cii => cii.Product)
                   .HasForeignKey(cii => cii.ProductId);
        }
    }
}