using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string Images { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Unit { get; set; }
        public long InventoryItemID { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public ICollection<Price> Prices { get; set; } = new List<Price>();
        public ICollection<CustomerInvoiceItem> InvoiceItems { get; set; } = new List<CustomerInvoiceItem>();

    }
}