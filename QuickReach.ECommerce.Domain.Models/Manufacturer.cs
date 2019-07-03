using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("Manufacturer")]
    public class Manufacturer : EntityBase
    {
        public Manufacturer()
        {
            this.ProductManufacturers = new List<ProductManufacturer>();
        }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public IEnumerable<ProductManufacturer> ProductManufacturers { get; set; }

        public void AddProduct(int productId)
        {
            var productManufacturer = new ProductManufacturer()
            {
                ManufacturerID = this.ID,
                ProductID = productId
            };
            ((ICollection<ProductManufacturer>)this.ProductManufacturers).Add(productManufacturer);
        }

        public void RemoveProduct(int productId)
        {
            var productManufacturer = this.ProductManufacturers
                                        .FirstOrDefault(pm => pm.ProductID == productId
                                                              && pm.ManufacturerID == this.ID);

            ((ICollection<ProductManufacturer>)this.ProductManufacturers).Remove(productManufacturer);
        }
    }
}
