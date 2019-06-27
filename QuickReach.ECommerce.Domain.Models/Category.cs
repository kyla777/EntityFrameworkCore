using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    // Specify actual table name
    [Table("Category")]
    public class Category : EntityBase
    {
        public Category()
        {
            this.ChildCategories = new List<CategoryRollUp>();
        }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<CategoryRollUp> ParentCategories { get; set; }
        public IEnumerable<CategoryRollUp> ChildCategories { get; set; }

        public void AddChild(int categoryId)
        {
            if(this.ID == categoryId)
            {
                throw new ArgumentException("Child category ID must not be the same as the parent category ID");
            }

            var child = new CategoryRollUp()
            {
                ParentCategoryID = this.ID,
                ChildCategoryID = categoryId
            };
            ((ICollection<CategoryRollUp>)this.ChildCategories).Add(child);
        }
    }
}
