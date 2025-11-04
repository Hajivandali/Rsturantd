using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class CustomerInvoiceItemConfiguration : IEntityTypeConfiguration<CustomerInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<CustomerInvoiceItem> builder)
        {
            // Primary Key
            builder.HasKey(cii => cii.Id);

            // رابطه: CustomerInvoiceItem -> CustomerInvoice
            builder.HasOne(cii => cii.Invoice)
                   .WithMany(ci => ci.InvoiceItems)
                   .HasForeignKey(cii => cii.InvoiceReference)
                   .OnDelete(DeleteBehavior.Cascade);

            // رابطه: CustomerInvoiceItem -> Product
            builder.HasOne(cii => cii.Product)
                   .WithMany(p => p.InvoiceItems)
                   .HasForeignKey(cii => cii.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision برای قیمت‌ها
            builder.Property(cii => cii.Fee)
                   .HasPrecision(18, 2);

            builder.Property(cii => cii.TotalPrice)
                   .HasPrecision(18, 2);
        }
    }
}
