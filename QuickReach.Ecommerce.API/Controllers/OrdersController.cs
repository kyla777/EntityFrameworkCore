using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.Ecommerce.API.ViewModel;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;

namespace QuickReach.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository repository;
        private readonly IProductRepository productRepo;
        public OrdersController(IOrderRepository repository,
                                IProductRepository productRepo)
        {
            this.repository = repository;
            this.productRepo = productRepo;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = this.repository.Retrieve(id);
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order newOrder)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newOrder);

            return CreatedAtAction(nameof(this.Get), new { id = newOrder.ID }, newOrder);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, order);

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }

        [HttpPut("{orderId}/items/")]
        public IActionResult AddOrderItem(int orderId, [FromBody] OrderItem orderItem)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var order = this.repository.Retrieve(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var product = this.productRepo.Retrieve(orderItem.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            order.AddOrderItem(orderItem);

            this.repository.Update(order.ID, order);

            return Ok(order);
        }

        // TODO:
        // Remove item from orderitem table
        [HttpPut("{orderId}/items/{orderItemId}")]
        public IActionResult RemoveOrderItem(int orderId, int orderItemId)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var order = this.repository.Retrieve(orderId);

            order.RemoveOrderItem(orderItemId);

            this.repository.Update(order.ID, order);

            return Ok(order);
        }
    }
}