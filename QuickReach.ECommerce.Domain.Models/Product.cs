using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickReach.ECommerce.Domain.Models
{
    // Specify actual table name
    [Table("Product")]
    public class Product : EntityBase
    {
        // Data Annotations
        [Required] 
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
