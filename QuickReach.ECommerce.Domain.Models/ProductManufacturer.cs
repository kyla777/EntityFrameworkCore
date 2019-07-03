using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("ProductManufacturer")]
    public class ProductManufacturer
    {
        public int ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
