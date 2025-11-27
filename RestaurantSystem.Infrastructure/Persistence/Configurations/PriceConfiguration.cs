using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(p => p.Id);

            // Relationship: Price -> Product
            builder.HasOne(p => p.Product)
                   .WithMany(pr => pr.Prices)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Optional: Decimal precision
            builder.Property(p => p.Amount)
                   .HasPrecision(18, 2);
        }
    }
}