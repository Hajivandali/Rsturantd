using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerInvoice> CustomerInvoices { get; set; }
        public DbSet<CustomerInvoiceItem> CustomerInvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Menu and MenuItem relationship
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuItems)
                .WithOne(mi => mi.Menu)
                .HasForeignKey(mi => mi.MenuReference);

            // MenuItem and Product relationship
            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Product)
                .WithMany(p => p.MenuItems)
                .HasForeignKey(mi => mi.ProductReference);

            // Product and Price relationship
            modelBuilder.Entity<Price>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Prices)
                .HasForeignKey(p => p.ProductReference);

            // Customer and CustomerInvoice relationship
            modelBuilder.Entity<CustomerInvoice>()
                .HasOne(ci => ci.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(ci => ci.CustomerId);

            // CustomerInvoice and CustomerInvoiceItem relationship
            modelBuilder.Entity<CustomerInvoiceItem>()
                .HasOne(cii => cii.Invoice)
                .WithMany(ci => ci.InvoiceItems)
                .HasForeignKey(cii => cii.InvoiceReference);

            // CustomerInvoiceItem and Product relationship
            modelBuilder.Entity<CustomerInvoiceItem>()
                .HasOne(cii => cii.Product)
                .WithMany(p=> p.InvoiceItems)
                .HasForeignKey(cii => cii.ProductId);

            // Configure decimal precision for CustomerInvoiceItem
            modelBuilder.Entity<CustomerInvoiceItem>()
                .Property(cii => cii.Fee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CustomerInvoiceItem>()
                .Property(cii => cii.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}   