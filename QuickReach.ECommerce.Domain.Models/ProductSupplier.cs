using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("ProductSupplier")]
    public class ProductSupplier
    {
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
