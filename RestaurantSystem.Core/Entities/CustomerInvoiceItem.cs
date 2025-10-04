using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class CustomerInvoiceItem
    {
        public long Id { get; set; }
        public int CountProduct { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalPrice { get; set; }
        public long InvoiceReference { get; set; }
        public CustomerInvoice Invoice { get; set; } = null!;
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public ICollection<CustomerInvoiceItem> CustomerInvoiceItems { get; set; } = new List<CustomerInvoiceItem>();

    }
}