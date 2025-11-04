using System;
using System.Collections.Generic;

namespace RestaurantSystem.Core.Entities
{
    public class CustomerInvoice
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public long CustomerId { get; set; }

        
        public decimal TotalAmount { get; set; }
 
        public string? InvoiceNumber { get; set; }
 
        public Customer? Customer { get; set; }
        public ICollection<CustomerInvoiceItem> InvoiceItems { get; set; } = new List<CustomerInvoiceItem>();
    }
}
