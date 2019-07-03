using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickReach.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository repository;
        public CustomersController(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var customers = this.repository.Retrieve(search, skip, count);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetSpecificUser(int id)
        {
            var customer = this.repository.Retrieve(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newCustomer);

            return CreatedAtAction(nameof(this.Get), new { id = newCustomer.ID }, newCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, customer);

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}
