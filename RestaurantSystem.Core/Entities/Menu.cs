using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Menu
    {

        public long Id { get; set; }
        public string? Title  { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTime LastEdited { get; set; }

        // هر منو چند منو آیتم دارد 
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();


    }
}