using System;
using System.Collections.Generic;

namespace RestaurantSystem.Core.Entities
{
    public class Price
    {
        public long Id { get; set; }

        // تغییر نام به ProductId برای هماهنگی با دیگر مدل‌ها
        public long ProductId { get; set; }  

        public long Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastEdited { get; set; }

        public Product Product { get; set; } = null!;
    }
}
