using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;


namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            // Primary Key
            builder.HasKey(mi => mi.Id);

            // Relationship: MenuItem -> Product
            builder.HasOne(mi => mi.Product)
                   .WithMany(p => p.MenuItems)
                   .HasForeignKey(mi => mi.ProductId);
        }
    }
}