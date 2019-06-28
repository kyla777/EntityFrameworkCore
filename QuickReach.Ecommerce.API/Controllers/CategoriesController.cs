using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;

namespace QuickReach.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;
        private readonly IProductRepository productRepo;
        public CategoriesController(ICategoryRepository repository,
            IProductRepository productRepo)
        {
            this.repository = repository;
            this.productRepo = productRepo;
        }

        // Create GET
        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var categories = this.repository.Retrieve(search, skip, count);
            return Ok(categories);
        }

        // Create GET with id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = this.repository.Retrieve(id);
            return Ok(category);
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] Category newCategory)
        {
            // ModelState is a property of the base controller
            // Checks if category meets requirements
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newCategory);

            return CreatedAtAction(nameof(this.Get), new { id = newCategory.ID }, newCategory);
        }
        
        // PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, category);

            return Ok(category);
        }

        // Add product
        [HttpPut("{categoryId}/products/")]
        public IActionResult AddProductCateoryAndUpdateCategory(int categoryId, [FromBody] ProductCategory entity)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = this.repository.Retrieve(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(entity.ProductID);

            if (product == null)
            {
                return NotFound();
            }

            category.AddProduct(entity.ProductID);

            this.repository.Update(category.ID, category);

            return Ok(category);
        }

        // Remove productcategory
        [HttpPut("{categoryId}/products/{productId}")]
        public IActionResult RemoveProductCategoryAndUpdateCategory(int categoryId, int productId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = this.repository.Retrieve(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(productId);

            if (product == null)
            {
                return NotFound();
            }

            category.RemoveProduct(productId);

            this.repository.Update(category.ID, category);

            return Ok(category);
        }

        // Add Child categories
        [HttpPut("{categoryId}/subcategories/")]
        public IActionResult AddChildCategories(int categoryId, [FromBody] Category subCategory)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var parentCategory = this.repository.Retrieve(categoryId);

            if (parentCategory == null)
            {
                return NotFound();
            }

            parentCategory.AddChild(subCategory.ID);

            this.repository.Update(parentCategory.ID, parentCategory);

            return Ok(parentCategory);
        }

        // Remove Child categories
        [HttpPut("{categoryId}/subcategories/{subCategoryId}")]
        public IActionResult RemoveChildCategories(int categoryId, int subCategoryId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var parentCategory = this.repository.Retrieve(categoryId);

            if (parentCategory == null)
            {
                return NotFound();
            }

            var subCategory = this.repository.Retrieve(subCategoryId);

            if (subCategory == null)
            {
                return NotFound();
            }

            parentCategory.RemoveChild(subCategory.ID);

            this.repository.Update(parentCategory.ID, parentCategory);

            return Ok(parentCategory);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}