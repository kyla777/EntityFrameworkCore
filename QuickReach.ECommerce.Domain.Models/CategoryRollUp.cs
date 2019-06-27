using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("CategoryRollUp")]
    public class CategoryRollUp
    {
        public int ParentCategoryID { get; set; }
        public Category ParentCategory { get; set; }
                
        public int ChildCategoryID { get; set; }
        public Category ChildCategory { get; set; }
    }
}
