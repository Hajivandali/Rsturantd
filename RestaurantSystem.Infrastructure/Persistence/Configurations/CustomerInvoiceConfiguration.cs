using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantSystem.Core.Entities;

namespace RestaurantSystem.Infrastructure.Persistence.Configurations
{
    public class CustomerInvoiceConfiguration : IEntityTypeConfiguration<CustomerInvoice>
    {
        public void Configure(EntityTypeBuilder<CustomerInvoice> builder)
        {
            // Primary Key
            builder.HasKey(ci => ci.Id);

            // رابطه: CustomerInvoice → Customer
            builder.HasOne(ci => ci.Customer)
                   .WithMany(c => c.Invoices)
                   .HasForeignKey(ci => ci.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // رابطه: CustomerInvoice → CustomerInvoiceItems
            builder.HasMany(ci => ci.InvoiceItems)
                   .WithOne(ii => ii.Invoice)
                   .HasForeignKey(ii => ii.InvoiceReference)
                   .OnDelete(DeleteBehavior.Cascade);

            // فیلد عددی با precision بالا برای مبلغ کل
            builder.Property(ci => ci.TotalAmount)
                   .HasPrecision(18, 2);

            // تاریخ ایجاد فاکتور
            builder.Property(ci => ci.CreatedDate)
                   .IsRequired();

            // اختیاری: شماره فاکتور
            builder.Property(ci => ci.InvoiceNumber)
                   .HasMaxLength(50);
        }
    }
}
