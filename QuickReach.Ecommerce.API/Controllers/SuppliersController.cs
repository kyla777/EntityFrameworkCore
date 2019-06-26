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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository repository;
        public SuppliersController(ISupplierRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var suppliers = this.repository.Retrieve(search, skip, count);
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supplier = this.repository.Retrieve(id);
            return Ok(supplier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Supplier newSupplier)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newSupplier);

            return CreatedAtAction(nameof(this.Get), new { id = newSupplier.ID }, newSupplier);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Supplier supplier)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, supplier);

            return Ok(supplier);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}