using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace ShopBridge.Models
{
    [ExcludeFromCodeCoverage]
    public partial class Inventory
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]        
        public double Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContryOfOrigin { get; set; }
    }
}
