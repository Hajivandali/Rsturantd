using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class CustomerInvoice
    {
         public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CustomerId { get; set; }
        public long TotalAmount { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<CustomerInvoiceItem> InvoiceItems { get; set; } = new List<CustomerInvoiceItem>();
    
    }
}