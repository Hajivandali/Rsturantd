using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class MenuItem
{
    public long Id { get; set; }

    [Required]
    public long MenuId { get; set; }  // قبلاً MenuReference

    [Required]
    public long ProductId { get; set; }  // قبلاً ProductReference

    public bool IsActive { get; set; }

    public Menu Menu { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

}