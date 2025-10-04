using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Price
    {
        public long Id { get; set; }
        public long ProductReference { get; set; } // fornky be product 
        public long Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEdited { get; set; }
 
        public Product? Product { get; set; }
    }
}                       