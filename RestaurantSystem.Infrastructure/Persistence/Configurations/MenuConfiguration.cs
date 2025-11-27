using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;


namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);

            // Relationship: Menu -> MenuItem
            builder.HasMany(m => m.MenuItems)
                   .WithOne(mi => mi.Menu)
                   .HasForeignKey(mi => mi.MenuId);
        }
    }
}