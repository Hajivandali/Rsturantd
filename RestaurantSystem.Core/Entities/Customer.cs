using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Customer
    {
        public long Id { get; set; }
         
        public string Name { get; set; } = null!;
        public string Family { get; set; } = null!;
        public long PhoneNumber { get; set; }

                public ICollection<CustomerInvoice> Invoices { get; set; } = new List<CustomerInvoice>();

    }
}