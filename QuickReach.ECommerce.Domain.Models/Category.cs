using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
            this.ParentCategories = new List<CategoryRollUp>();
            this.ProductCategories = new List<ProductCategory>();
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

        public void AddChild(int childId)
        {
            if(this.ID == childId)
            {
                throw new ArgumentException("Child category ID must not be the same as the parent category ID");
            }

            var child = new CategoryRollUp()
            {
                ParentCategoryID = this.ID,
                ChildCategoryID = childId
            };
            ((ICollection<CategoryRollUp>)this.ChildCategories).Add(child);
        }

        public void RemoveChild(int childId)
        {
            var child = this.ChildCategories
                                .FirstOrDefault(c => c.ParentCategoryID == this.ID
                                && c.ChildCategoryID == childId);

            ((ICollection<CategoryRollUp>)this.ChildCategories).Remove(child);
        }

        public void AddProduct(int productId)
        {
            var productCategory = new ProductCategory()
            {
                CategoryID = this.ID,
                ProductID = productId,
            };

            ((ICollection<ProductCategory>)this.ProductCategories).Add(productCategory);
        }

        public void RemoveProduct(int productId)
        {
            var productCategory = this.ProductCategories
                                    .FirstOrDefault(pc => pc.ProductID == productId 
                                                        && pc.CategoryID == this.ID);

            ((ICollection<ProductCategory>)this.ProductCategories).Remove(productCategory);
        }
    }
}
