using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;

namespace QuickReach.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository repository;
        private readonly IProductRepository productRepo;
        public ManufacturersController(IManufacturerRepository repository,
                                       IProductRepository productRepo)
        {
            this.repository = repository;
            this.productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetAll(string search = "", int skip = 0, int count = 10)
        {
            var manufacturers = this.repository.Retrieve(search, skip, count);
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public IActionResult GetSpecificManufacturer(int id)
        {
            var manufacturer = this.repository.Retrieve(id);
            return Ok(manufacturer);
        }

        [HttpPost]
        public IActionResult CreateManufacturer([FromBody] Manufacturer newManufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newManufacturer);

            return CreatedAtAction(nameof(this.GetSpecificManufacturer), new { id = newManufacturer.ID }, newManufacturer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Manufacturer manufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, manufacturer);

            return Ok(manufacturer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }

        [HttpPut("{manufacturerId}/products/")]
        public IActionResult AddProductManufacturer(int manufacturerId, [FromBody] ProductManufacturer entity)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var manufacturer = this.repository.Retrieve(manufacturerId);

            if(manufacturer == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(entity.ProductID);

            if(product == null)
            {
                return NotFound();
            }

            manufacturer.AddProduct(entity.ProductID);

            this.repository.Update(manufacturer.ID, manufacturer);

            return Ok(manufacturer);
        }

        [HttpPut("{manufacturerId}/products/{productId}")]
        public IActionResult RemoveProductManufacturer(int manufacturerId, int productId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var manufacturer = this.repository.Retrieve(manufacturerId);

            if(manufacturer == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(productId);

            if(product == null)
            {
                return NotFound();
            }

            manufacturer.RemoveProduct(productId);

            this.repository.Update(manufacturer.ID, manufacturer);

            return Ok(manufacturer);
        }
    }
}